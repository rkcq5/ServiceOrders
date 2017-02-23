<!--- 
	Description:		Force application to use HTTPS.
	Original Developer:	Duane Nettles
--->

<cfif FindNoCase("off", cgi.https) or
      Not len(trim(cgi.https))>
  <cflocation url="https://#cgi.server_name##cgi.script_name##cgi.query_string#" addtoken="no">
  <cfabort>
</cfif>