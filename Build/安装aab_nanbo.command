#!/bin/bash

# AAB自动安装脚本（带签名支持）
# 使用说明：
# 1. 修改下面的路径和签名配置
# 2. 确保设备已连接并开启USB调试
# 3. 运行脚本即可自动安装最新的AAB文件

# 设置 Java 路径（使用Unity自带的Java）
JAVA_PATH="/Applications/Unity/Hub/Editor/2022.3.59f1/PlaybackEngines/AndroidPlayer/OpenJDK/bin/java"

# 获取当前脚本所在目录
SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
# 项目根目录（脚本在AndroidProject目录中，所以上一级就是项目根目录）
PROJECT_ROOT="$(dirname "$SCRIPT_DIR")"

echo "脚本目录：$SCRIPT_DIR"
echo "项目根目录：$PROJECT_ROOT"

# 设置 AAB 根目录（相对于项目根目录）
AAB_ROOT="$SCRIPT_DIR/Android"

# 设置 bundletool 路径（相对于脚本目录）
BUNDLETOOL_PATH="$SCRIPT_DIR/bundletool.jar"

# ================== 签名配置（请根据实际情况修改）==================
# Keystore文件路径（相对于项目根目录）
KEYSTORE_PATH="$SCRIPT_DIR/SigningKey/user.keystore"

# Keystore密码
KEYSTORE_PASSWORD="abc123"

# Key别名（在keystore中的key名称）
KEY_ALIAS="abc123"

# Key密码（通常与keystore密码相同）
KEY_PASSWORD="abc123"

# 包名（用于卸载，需要与AAB中的包名一致）
PACKAGE_NAME="com.DinStudio.nanboguan"
# ==============================================================

# 检查 Java 是否存在
if [ ! -f "$JAVA_PATH" ]; then
  echo "❌ 未找到Unity Java环境：$JAVA_PATH"
  echo "请检查Unity安装路径是否正确"
  read -n 1 -s -r -p "按任意键关闭窗口..."
  exit 1
fi

echo "✅ 使用Unity自带的Java：$JAVA_PATH"

# 检查 bundletool 是否存在
if [ ! -f "$BUNDLETOOL_PATH" ]; then
  echo "❌ 未找到 bundletool.jar，请下载并放置到：$BUNDLETOOL_PATH"
  echo "下载地址：https://github.com/google/bundletool/releases"
  read -n 1 -s -r -p "按任意键关闭窗口..."
  exit 1
fi

# 检查签名文件是否存在
if [ ! -f "$KEYSTORE_PATH" ]; then
  echo "❌ 未找到签名文件：$KEYSTORE_PATH"
  echo "请确保签名文件存在，或修改脚本中的 KEYSTORE_PATH 路径"
  read -n 1 -s -r -p "按任意键关闭窗口..."
  exit 1
fi

echo "✅ 找到签名文件：$KEYSTORE_PATH"

# 找到最新的目录（按修改时间倒序排序，取第一个）
LATEST_DIR=$(ls -td "$AAB_ROOT"/*/ | head -n 1)

if [ -z "$LATEST_DIR" ]; then
  echo "未找到子目录。请检查路径：$AAB_ROOT"
  exit 1
fi

echo "最新目录为：$LATEST_DIR"

# 在该目录中查找第一个 .aab 文件
AAB_FILE=$(find "$LATEST_DIR" -type f -name "*.aab" | head -n 1)

if [ -z "$AAB_FILE" ]; then
  echo "未在目录 $LATEST_DIR 中找到 .aab 文件。"
  exit 1
fi

echo "找到 AAB 文件：$AAB_FILE"

# 检查设备连接
echo "检查设备连接..."
DEVICE_COUNT=$(adb devices | grep -v "List of devices" | grep -c "device$")
if [ "$DEVICE_COUNT" -eq 0 ]; then
  echo "❌ 未检测到连接的安卓设备，请确保设备已连接并开启USB调试"
  read -n 1 -s -r -p "按任意键关闭窗口..."
  exit 1
fi

echo "检测到 $DEVICE_COUNT 个设备已连接"

# 卸载应用
echo "开始卸载旧版本应用：$PACKAGE_NAME"
adb uninstall "$PACKAGE_NAME"

# 使用 bundletool 从 AAB 生成 APKs 并安装
echo "开始使用 bundletool 处理 AAB 文件..."

# 生成临时APKs文件路径
TEMP_APKS="${AAB_FILE%.*}.apks"

echo "步骤1: 从 AAB 生成 APKs 文件（带签名）..."
"$JAVA_PATH" -jar "$BUNDLETOOL_PATH" build-apks \
  --bundle="$AAB_FILE" \
  --output="$TEMP_APKS" \
  --connected-device \
  --ks="$KEYSTORE_PATH" \
  --ks-pass=pass:"$KEYSTORE_PASSWORD" \
  --ks-key-alias="$KEY_ALIAS" \
  --key-pass=pass:"$KEY_PASSWORD"

if [ $? -ne 0 ]; then
  echo "❌ 生成 APKs 文件失败"
  echo "可能的原因："
  echo "1. 签名文件密码错误"
  echo "2. Key alias 不存在"
  echo "3. AAB 文件损坏"
  echo "4. 签名文件格式错误"
  read -n 1 -s -r -p "按任意键关闭窗口..."
  exit 1
fi

echo "✅ APKs 文件生成成功：$TEMP_APKS"

echo "步骤2: 安装 APKs 到设备..."
"$JAVA_PATH" -jar "$BUNDLETOOL_PATH" install-apks --apks="$TEMP_APKS"

# 检查安装结果
if [ $? -eq 0 ]; then
  echo "✅ AAB 安装成功！"
  echo "应用已成功安装到设备"
  
  # 清理临时APKs文件
  if [ -f "$TEMP_APKS" ]; then
    echo "清理临时文件：$TEMP_APKS"
    rm "$TEMP_APKS"
  fi
  
  echo "安装完成！"
  echo "3秒后自动关闭窗口..."
  sleep 3
  exit 0
else
  echo "❌ AAB 安装失败"
  echo "可能的原因："
  echo "1. 设备连接问题"
  echo "2. AAB 文件损坏"
  echo "3. 设备存储空间不足"
  echo "4. 签名问题"
  
  # 清理临时APKs文件
  if [ -f "$TEMP_APKS" ]; then
    echo "清理临时文件：$TEMP_APKS"
    rm "$TEMP_APKS"
  fi
  
  read -n 1 -s -r -p "按任意键关闭窗口..."
  exit 1
fi