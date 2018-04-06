
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<body lang="ru-RU" link="#0000ff" dir="ltr">
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">************************************************************</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><a name="_GoBack"></a>
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US"><b>SafeMoney
REST API</b></span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">************************************************************</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">SafeMoney
is built in REST API architecture style. </span></font></font>
</p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Example:
<a href="http://safemooney.azurewebsites/%7Bmethod%7D">http://safemooney.azurewebsites/{method}</a></span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">For
communication with server use HTTP protocol<a class="sdfootnoteanc" name="sdfootnote1anc" href="#sdfootnote1sym"><sup>1</sup></a>.</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Methods:</span></font></font></p>
<p align="right" style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US"><b>Part
I. Account</b></span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/account/login</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
This method provides access to service for user</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[POST] api/account/login</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Body:</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">{&quot;UserId&quot;:&quot;value&quot;,
&quot;Username&quot;:&quot;value&quot;,
&quot;Password&quot;:&quot;value&quot;,&quot;FirstName&quot;:&quot;value&quot;,
&quot;LastName&quot;:&quot;value&quot;}</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[POST] api/account/login</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">{&quot;UserId&quot;:&quot;value&quot;,
&quot;Username&quot;:&quot;value&quot;, FirstName&quot;:&quot;value&quot;,
&quot;LastName&quot;:&quot;value&quot;, &quot;Access_Token&quot;:&quot;value&quot;}</span></font></font></font></p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/account/logout</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
logging out</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/account/logout</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Body:</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">Empty</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/account/logout</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">Empty
(only status code)</span></font></font></font></p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*</span></font></font><span lang="en-US">
</span><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">api/account/signup</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
This method register user in the system</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[POST] api/account/signup </span></font></font>
</p>
<p style="text-indent: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Body:</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">{&quot;Username&quot;:&quot;value&quot;,
&quot;Password&quot;:&quot;value&quot;,&quot;FirstName&quot;:&quot;value&quot;,
&quot;LastName&quot;:&quot;value&quot;}</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[POST] api/account/signup</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">Empty
(only status code)</span></font></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/account/changeuserinfo</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Change user first name, last name and username</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[POST] api/{userId}/account/logout </span></font></font>
</p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Body:</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">{&quot;Username&quot;:&quot;value&quot;,
FirstName&quot;:&quot;value&quot;, &quot;LastName&quot;:&quot;value&quot;}</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[POST] api/{userId}/account/logout</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">{&quot;UserId&quot;:&quot;value&quot;,
&quot;Username&quot;:&quot;value&quot;, FirstName&quot;:&quot;value&quot;,
&quot;LastName&quot;:&quot;value&quot;}</span></font></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/account/changepass</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Change user password</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[POST] api/{userId}/account/logout </span></font></font>
</p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Body:</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">{&quot;OldPassword&quot;:&quot;value&quot;,
NewPassword&quot;:&quot;value&quot;}</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[POST] api/{userId}/account/changepass</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">Empty
(or error message)</span></font></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/getimg/{filename}</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Return user image or default image</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/getimg/{filename} (</span></font></font><font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">filename:
&quot;{userid}.jpg&quot;</span></font></font></font><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">)</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Body:</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">Empty</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/getimg/{filename}</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">raw
data (bytes of image)</span></font></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*
api/{userId}/setimg?filename={userid}.jpg</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Set image for user</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[POST] api/{userId}/setimg </span></font></font>
</p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Body:</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">raw
data (bytes of image)</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[POST] api/{userId}/setimg</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">Empty
(or error message)</span></font></font></font></p>
<p align="right" style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US"><b>Part
II. Services</b></span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/services/vk/addservice?code={value}</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Add vk.com service for user into services list. Code is parameter
returned from vk.com on authorization process</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/services/vk/addservice</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/services/vk/addservice</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">Empty
(or error message)</span></font></font></font></p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/services/vk/search?query={value}</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Search people from user&rsquo;s vk.com friends list</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/services/vk/search</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/services/vk/search</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">[{&quot;UserId&quot;:&quot;value&quot;,
&quot;Username&quot;:&quot;value&quot;,&quot;FirstName&quot;:&quot;value&quot;,
&quot;LastName&quot;:&quot;value&quot;, &quot;Availability&quot;:&quot;value&quot;,
&quot;AuthorizationId&quot;:&quot;value&quot;, &quot;PhotoUri&quot;:&quot;value&quot;},
{&hellip;}, &hellip;]</span></font></font></font></p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/services/vk/check</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Check for availability of vk.com account</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/services/vk/check</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/services/vk/check</span></font></font></p>
<p style="text-indent: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">&quot;true&quot;/&quot;false&quot;
or error message</span></font></font></font></p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/services/email/sendinvent?email={value}&amp;signup_url={value}</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Send email invitation to join SafeMoney. Email parameter    is
email:) signup_url is url to signup page or app store link</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/services/email/sendinvent</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/services/email/sendinvent</span></font></font></p>
<p style="text-indent: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">Empty
(or error message)</span></font></font></font></p>
<p align="right" style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US"><b>Part
III. Transactions</b></span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/transactions/getuserlist</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Return full friend list for user</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/transactions/getuserlist</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/transactions/getuserlist</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">[{&quot;UserId&quot;:&quot;value&quot;,
&quot;Username&quot;:&quot;value&quot;,&quot;FirstName&quot;:&quot;value&quot;,
&quot;LastName&quot;:&quot;value&quot;}, {&hellip;}, &hellip;]</span></font></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/transactions/getuserlist?search={value}</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Return result of searching.</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/transactions/getuserlist</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/transactions/getuserlist</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">[{&quot;UserId&quot;:&quot;value&quot;,
&quot;Username&quot;:&quot;value&quot;,&quot;FirstName&quot;:&quot;value&quot;,
&quot;LastName&quot;:&quot;value&quot;}, {&hellip;}, &hellip;]</span></font></font></font></p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/transactions/add</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Add new not permitted transaction</span></font></font></font></p>
<p style="text-indent: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[POST] api/{userId}/transactions/add</span></font></font></p>
<p style="text-indent: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Body:</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">{&quot;userId&quot;:&quot;value&quot;,
&quot;count&quot;:&quot;value&quot;, &quot;date&quot;:&quot;value&quot;,
&quot;period&quot;:&quot;value&quot;, &quot;comment&quot;:&quot;value&quot;}</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[PSOT] api/{userId}/transactions/add</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">Empty
(or error message)</span></font></font></font></p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/transactions/checkqueue</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Check transactions table and retern not permited for this user</span></font></font></font></p>
<p style="text-indent: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/transactions/checkqueue</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/transactions/checkqueue</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">[{</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">&quot;transactionData&quot;:{</span></font></font></font></p>
<p style="margin-left: 0.98in; text-indent: 0in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">&quot;Id&quot;:&quot;value&quot;,&quot;User1Id&quot;:&quot;value&quot;,&quot;User2Id&quot;:&quot;value&quot;,&quot;Count&quot;:&quot;value&quot;,
&quot;Date&quot;:&quot;value&quot;, &quot;Period&quot;:&quot;value&quot;,
&quot;IsPermited&quot;:&quot;value&quot;, &quot;IsClosed&quot;:&quot;value&quot;,
&quot;Comment&quot;:&quot;value&quot;}, </span></font></font></font>
</p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">&quot;userData&quot;:{
</span></font></font></font>
</p>
<p style="margin-left: 0.98in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">&quot;UserId&quot;:&quot;value&quot;,&quot;Username&quot;:&quot;value&quot;,&quot;FirstName&quot;:&quot;value&quot;,&quot;LastName&quot;:&quot;value&quot;,
&quot;PhotoUri&quot;:&quot;value:&quot;value&quot;}</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">},{&hellip;},
&hellip;]</span></font></font></font></p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/transactions/confirm/{transId}</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Confirm transaction</span></font></font></font></p>
<p style="text-indent: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/transactions/confirm/{transId}</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/transactions/confirm/{transId}</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">	Empty
(or error message)</span></font></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/transactions/close/{transId}</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Close transaction</span></font></font></font></p>
<p style="text-indent: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/transactions/close/{transId}</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/transactions/close/{transId}</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">	Empty
(or error message)</span></font></font></font></p>
<p lang="en-US" style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">*api/{userId}/transactions/fetch</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Description:</span></font></font><font color="#262626"><font face="Consolas, serif"><font size="3" style="font-size: 12pt"><span lang="en-US">
Return all unclosed transactions for user</span></font></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Request:
[GET] api/{userId}/transactions/fetch</span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US">Response:
[GET] api/{userId}/transactions/fetch</span></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">[{</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">&quot;transactionData&quot;:{</span></font></font></font></p>
<p style="margin-left: 0.98in; text-indent: 0in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">&quot;Id&quot;:&quot;value&quot;,&quot;User1Id&quot;:&quot;value&quot;,&quot;User2Id&quot;:&quot;value&quot;,&quot;Count&quot;:&quot;value&quot;,
&quot;Date&quot;:&quot;value&quot;, &quot;Period&quot;:&quot;value&quot;,
&quot;IsPermited&quot;:&quot;value&quot;, &quot;IsClosed&quot;:&quot;value&quot;,
&quot;Comment&quot;:&quot;value&quot;}, </span></font></font></font>
</p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">&quot;userData&quot;:{
</span></font></font></font>
</p>
<p style="margin-left: 0.98in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">&quot;UserId&quot;:&quot;value&quot;,&quot;Username&quot;:&quot;value&quot;,&quot;FirstName&quot;:&quot;value&quot;,&quot;LastName&quot;:&quot;value&quot;,
&quot;PhotoUri&quot;:&quot;value:&quot;value&quot;}</span></font></font></font></p>
<p style="margin-left: 0.49in; margin-bottom: 0.14in; line-height: 115%">
<font color="#000000"><font face="Consolas, serif"><font size="2" style="font-size: 9pt"><span lang="en-US">},{&hellip;},
&hellip;]</span></font></font></font></p>
<p align="right" style="margin-bottom: 0.14in; line-height: 115%"><font face="Consolas, serif"><font size="4" style="font-size: 14pt"><span lang="en-US"><b>Part
IV. Payments</b></span></font></font></p>
<p style="margin-bottom: 0.14in; line-height: 115%"><br/>
<br/>

</p>
</body>
</html>
