# Remove all comments and join all lines with \r\n
sed -e '/^\/\/.*$/d' -e ':a;N;$!ba;s/\n/\\r\\n/g' $@