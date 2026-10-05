MANAGER_PACKAGE_NAME="org.matrix.vector.manager"
INJECTED_PACKAGE_NAME="com.android.shell"

am start -c "${MANAGER_PACKAGE_NAME}.LAUNCH_MANAGER" "${INJECTED_PACKAGE_NAME}/.BugreportWarningActivity"
