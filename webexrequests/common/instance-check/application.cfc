<cfcomponent output="no">
  <cfset this.Name = "instance-check" />
  <cfset this.ClientManagement = "false" />
  <cfset this.setClientCookies = "false" />
  <cfset this.ApplicationTimeout = CreateTimeSpan("0","0","00","0") />
  <cfset this.SessionManagement = "false" />
  <cfset this.SessionTimeout = CreateTimeSpan("0","0","00","0") />
  <cfset this.ScriptProtect = "all" />
  
  <cffunction name="onApplicationStart" returnType="boolean" output="no">
    <cfscript>
      StructClear(application);
	</cfscript>              
    <cfreturn true>
  </cffunction>
</cfcomponent>  