# install cli tool if not done yet
#npm install -g @azure/static-web-apps-cli azure-functions-core-tools

# option 1
swa start http://localhost:3000 --api-location http://localhost:7071

# option 2
#npm --prefix run build
#swa start ./staticwebapp.react/build --api-location http://localhost:7071