<cfcomponent output="no">
  <cffunction name="checkInstance" access="remote" returntype="any" output="no" returnformat="plain">
    <cfargument name="instances" type="string" required="yes" default="">
    <cfargument name="callback" type="string" required="no">
    <cfstoredproc procedure="COMMON.checkinstanceavailablility" datasource="IA-Web-Common">
      <cfprocparam cfsqltype="cf_sql_varchar" value="#instances#" type="in" maxlength="100">
      <cfprocresult resultset="1" name="outages">
    </cfstoredproc>

	<cfif #outages.recordcount# gt 0>
      <cfset r = "Y">
    <cfelse>
      <cfset r = "N">
    </cfif> 
    <cfset r = #serializeJSON(r)#>      
    <cfset r = arguments.callback&"("&r&")">
   <cfreturn r>           
  </cffunction>
  
    
  
</cfcomponent>