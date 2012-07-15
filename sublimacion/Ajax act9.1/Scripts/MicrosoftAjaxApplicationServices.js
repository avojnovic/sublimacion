﻿(function(){var a=null;function b(){var h="Sys.Services.RoleService.load",g="Sys.Services.AuthenticationService.logout",i="undefined",d="Sys.Services.AuthenticationService.login",f="Sys.Services.ProfileService.save",e="Sys.Services.ProfileService.load",c=true,b=false;Type._registerScript("MicrosoftAjaxApplicationServices.js",["MicrosoftAjaxWebServices.js"]);Type.registerNamespace("Sys.Services");Sys._defaultPath=Sys.Services._ProfileService.DefaultWebServicePath;Sys.Services._ProfileService=function(){Sys.Services._ProfileService.initializeBase(this);this.properties={}};Sys.Services._ProfileService.DefaultWebServicePath=Sys._defaultPath||"";delete Sys._defaultPath;Sys.Services._ProfileService.prototype={_defaultLoadCompletedCallback:a,_defaultSaveCompletedCallback:a,_path:"",_timeout:0,get_defaultLoadCompletedCallback:function(){return this._defaultLoadCompletedCallback},set_defaultLoadCompletedCallback:function(a){this._defaultLoadCompletedCallback=a},get_defaultSaveCompletedCallback:function(){return this._defaultSaveCompletedCallback},set_defaultSaveCompletedCallback:function(a){this._defaultSaveCompletedCallback=a},get_path:function(){return this._path||""},load:function(e,f,g,h){var a=this,d,c;if(!e){c="GetAllPropertiesForCurrentUser";d={authenticatedUserOnly:b}}else{c="GetPropertiesForCurrentUser";d={properties:a._clonePropertyNames(e),authenticatedUserOnly:b}}a._invoke(a._get_path(),c,b,d,Function.createDelegate(a,a._onLoadComplete),Function.createDelegate(a,a._onLoadFailed),[f,g,h])},save:function(f,d,e,g){var a=this,c=a._flattenProperties(f,a.properties);a._invoke(a._get_path(),"SetPropertiesForCurrentUser",b,{values:c.value,authenticatedUserOnly:b},Function.createDelegate(a,a._onSaveComplete),Function.createDelegate(a,a._onSaveFailed),[d,e,g,c.count])},_clonePropertyNames:function(f){var d=[],e={};for(var b=0;b<f.length;b++){var a=f[b];if(!e[a]){Array.add(d,a);e[a]=c}}return d},_flattenProperties:function(a,h,i){var b={},d,c,f=0;if(a&&a.length===0)return {value:b,count:0};for(var e in h){d=h[e];c=i?i+"."+e:e;if(Sys.Services.ProfileGroup.isInstanceOfType(d)){var k=this._flattenProperties(a,d,c),g=k.value;f+=k.count;for(var j in g){var l=g[j];b[j]=l}}else if(!a||Array.indexOf(a,c)!==-1){b[c]=d;f++}}return {value:b,count:f}},_get_path:function(){var a=this.get_path();if(!a.length)a=Sys.Services._ProfileService.DefaultWebServicePath;if(!a||!a.length)throw Error.invalidOperation(Sys.Res.servicePathNotSet);return a},_onLoadComplete:function(b,f,i){var a=this;if(typeof b!=="object")throw Error.invalidOperation(String.format(Sys.Res.webServiceInvalidReturnType,i,"Object"));var c=a._unflattenProperties(b);for(var g in c)a.properties[g]=c[g];var d=f[0]||a.get_defaultLoadCompletedCallback()||a.get_defaultSucceededCallback();if(d){var h=f[2]||a.get_defaultUserContext();d(b.length,h,e)}},_onLoadFailed:function(d,b){var a=b[1]||this.get_defaultFailedCallback();if(a){var c=b[2]||this.get_defaultUserContext();a(d,c,e)}},_onSaveComplete:function(b,c,h){var d=c[3];if(b!==a)if(b instanceof Array)d-=b.length;else if(typeof b==="number")d=b;else throw Error.invalidOperation(String.format(Sys.Res.webServiceInvalidReturnType,h,"Array"));var e=c[0]||this.get_defaultSaveCompletedCallback()||this.get_defaultSucceededCallback();if(e){var g=c[2]||this.get_defaultUserContext();e(d,g,f)}},_onSaveFailed:function(d,b){var a=b[1]||this.get_defaultFailedCallback();if(a){var c=b[2]||this.get_defaultUserContext();a(d,c,f)}},_unflattenProperties:function(e){var c={},d,f,h=0;for(var a in e){h++;f=e[a];d=a.indexOf(".");if(d!==-1){var g=a.substr(0,d);a=a.substr(d+1);var b=c[g];if(!b||!Sys.Services.ProfileGroup.isInstanceOfType(b)){b=new Sys.Services.ProfileGroup;c[g]=b}b[a]=f}else c[a]=f}e.length=h;return c}};Sys.Services._ProfileService.registerClass("Sys.Services._ProfileService",Sys.Net.WebServiceProxy);Sys.Services.ProfileGroup=function(a){if(a)for(var b in a)this[b]=a[b]};Sys.Services.ProfileGroup.registerClass("Sys.Services.ProfileGroup");Sys._path=Sys.Services.ProfileService._path;Sys._properties=Sys.Services.ProfileService.properties;Sys.Services.ProfileService=new Sys.Services._ProfileService;if(Sys._path){Sys.Services.ProfileService.set_path(Sys._path);delete Sys._path}if(Sys._properties){Sys.Services.ProfileService.properties=Sys._properties;(function(b){for(var c in b){var a=b[c];if(a&&a._propertygroup)Sys.Services.ProfileService.properties[c]=new Sys.Services.ProfileGroup(a._propertygroup)}})(Sys._properties);delete Sys._properties}Sys._defaultPath=Sys.Services._AuthenticationService.DefaultWebServicePath;Sys.Services._AuthenticationService=function(){Sys.Services._AuthenticationService.initializeBase(this)};Sys.Services._AuthenticationService.DefaultWebServicePath=Sys._defaultPath||"";delete Sys._defaultPath;Sys.Services._AuthenticationService.prototype={_defaultLoginCompletedCallback:a,_defaultLogoutCompletedCallback:a,_path:"",_timeout:0,_authenticated:b,get_defaultLoginCompletedCallback:function(){return this._defaultLoginCompletedCallback},set_defaultLoginCompletedCallback:function(a){this._defaultLoginCompletedCallback=a},get_defaultLogoutCompletedCallback:function(){return this._defaultLogoutCompletedCallback},set_defaultLogoutCompletedCallback:function(a){this._defaultLogoutCompletedCallback=a},get_isLoggedIn:function(){return this._authenticated},get_path:function(){return this._path||""},login:function(e,d,c,j,h,f,g,i){var a=this;a._invoke(a._get_path(),"Login",b,{userName:e,password:d,createPersistentCookie:c},Function.createDelegate(a,a._onLoginComplete),Function.createDelegate(a,a._onLoginFailed),[e,d,c,j,h,f,g,i])},logout:function(e,c,d,f){var a=this;a._invoke(a._get_path(),"Logout",b,{},Function.createDelegate(a,a._onLogoutComplete),Function.createDelegate(a,a._onLogoutFailed),[e,c,d,f])},_get_path:function(){var a=this.get_path();if(!a.length)a=Sys.Services._AuthenticationService.DefaultWebServicePath;if(!a||!a.length)throw Error.invalidOperation(Sys.Res.servicePathNotSet);return a},_onLoginComplete:function(k,h,l){var f=this;if(typeof k!=="boolean")throw Error.invalidOperation(String.format(Sys.Res.webServiceInvalidReturnType,l,"Boolean"));var g=h[4],j=h[7]||f.get_defaultUserContext(),e=h[5]||f.get_defaultLoginCompletedCallback()||f.get_defaultSucceededCallback();if(k){f._authenticated=c;if(e)e(c,j,d);if(typeof g!==i&&g!==a)window.location.href=g}else if(e)e(b,j,d)},_onLoginFailed:function(e,b){var a=b[6]||this.get_defaultFailedCallback();if(a){var c=b[7]||this.get_defaultUserContext();a(e,c,d)}},_onLogoutComplete:function(j,d,i){var c=this;if(j!==a)throw Error.invalidOperation(String.format(Sys.Res.webServiceInvalidReturnType,i,"null"));var e=d[0],h=d[3]||c.get_defaultUserContext(),f=d[1]||c.get_defaultLogoutCompletedCallback()||c.get_defaultSucceededCallback();c._authenticated=b;if(f)f(a,h,g);if(!e)window.location.reload();else window.location.href=e},_onLogoutFailed:function(c,b){var a=b[2]||this.get_defaultFailedCallback();if(a)a(c,b[3],g)},_setAuthenticated:function(a){this._authenticated=a}};Sys.Services._AuthenticationService.registerClass("Sys.Services._AuthenticationService",Sys.Net.WebServiceProxy);Sys._path=Sys.Services.AuthenticationService._path;Sys._auth=Sys.Services.AuthenticationService._auth;Sys.Services.AuthenticationService=new Sys.Services._AuthenticationService;if(Sys._path){Sys.Services.AuthenticationService.set_path(Sys._path);delete Sys._path}if(typeof Sys._auth!==i){Sys.Services.AuthenticationService._authenticated=Sys._auth;delete Sys._auth}Sys._defaultPath=Sys.Services._RoleService.DefaultWebServicePath;Sys.Services._RoleService=function(){Sys.Services._RoleService.initializeBase(this);this._roles=[]};Sys.Services._RoleService.DefaultWebServicePath=Sys._defaultPath||"";delete Sys._defaultPath;Sys.Services._RoleService.prototype={_defaultLoadCompletedCallback:a,_rolesIndex:a,_timeout:0,_path:"",get_defaultLoadCompletedCallback:function(){return this._defaultLoadCompletedCallback},set_defaultLoadCompletedCallback:function(a){this._defaultLoadCompletedCallback=a},get_path:function(){return this._path||""},get_roles:function(){return Array.clone(this._roles)},isUserInRole:function(a){var b=this._get_rolesIndex()[a.trim().toLowerCase()];return !!b},load:function(c,d,e){var a=this;Sys.Net.WebServiceProxy.invoke(a._get_path(),"GetRolesForCurrentUser",b,{},Function.createDelegate(a,a._onLoadComplete),Function.createDelegate(a,a._onLoadFailed),[c,d,e],a.get_timeout())},_get_path:function(){var a=this.get_path();if(!a||!a.length)a=Sys.Services._RoleService.DefaultWebServicePath;if(!a||!a.length)throw Error.invalidOperation(Sys.Res.servicePathNotSet);return a},_get_rolesIndex:function(){var a=this;if(!a._rolesIndex){var d={};for(var b=0;b<a._roles.length;b++)d[a._roles[b].toLowerCase()]=c;a._rolesIndex=d}return a._rolesIndex},_onLoadComplete:function(c,e,i){var b=this;if(c&&!(c instanceof Array))throw Error.invalidOperation(String.format(Sys.Res.webServiceInvalidReturnType,i,"Array"));b._roles=c;b._rolesIndex=a;var d=e[0]||b.get_defaultLoadCompletedCallback()||b.get_defaultSucceededCallback();if(d){var g=e[2]||b.get_defaultUserContext(),f=Array.clone(c);d(f,g,h)}},_onLoadFailed:function(d,b){var a=b[1]||this.get_defaultFailedCallback();if(a){var c=b[2]||this.get_defaultUserContext();a(d,c,h)}}};Sys.Services._RoleService.registerClass("Sys.Services._RoleService",Sys.Net.WebServiceProxy);Sys._path=Sys.Services.RoleService._path;Sys._roles=Sys.Services.RoleService._roles;Sys.Services.RoleService=new Sys.Services._RoleService;if(Sys._path){Sys.Services.RoleService.set_path(Sys._path);delete Sys._path}if(Sys._roles){Sys.Services.RoleService._roles=Sys._roles;delete Sys._roles}}if(window.Sys&&Sys.loader)Sys.loader.registerScript("ApplicationServices",a,b);else b()})();