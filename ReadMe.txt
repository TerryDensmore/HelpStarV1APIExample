This is a simple intro to the VersionOne API and uses the app.config file for server settings.
Below is the snippet that is used in the app.config file.

Server Settings 
<appSettings>
    <add key="DebugFileName" value="C:\VersionOneAPIClientDebug.txt" />
    <add key="V1Url" value="https://www14.v1host.com/v1sdktesting/" />
    <add key="V1UserName" value="admin" />
    <add key="V1Password" value="admin" />
    <add key="ProxyUrl" value="https://myProxyServer:3128" />
    <add key="ConfigUrl" value="config.v1/" />
    <add key="ProxyUserName" value="Administrator" />
    <add key="ProxyPassword" value="12345678" />
    <add key="UseWindowsIntegratedAuth" value="false" />
</appSettings>