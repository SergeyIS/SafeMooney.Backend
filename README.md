
## SafeMoney REST API

SafeMoney is web-based platform for sharing and controlling your money with support of social services.

This's documentation for developers who wants to use SafeMoney API in their applications.

## Overview

SafeMoney is built in REST API architecture style.

![Main page of application](https://raw.githubusercontent.com/SergeyIS/Storage/master/SafeMooney/laptop-mpage.png)


Example of request:  http://{domain}/{methodName}/{params}

For communication with server use HTTP protocol. (For more information use [google](https://www.google.com))

> List of available domains:
>  - safemooney.azurewebsites.net



![User settings page](https://raw.githubusercontent.com/SergeyIS/Storage/master/SafeMooney/laptop-editpage.png)
## Account API

> *api/account/login

Description: This method provides access to service for user

Request: [POST] api/account/login

Body:

    {"Username":"value", "Password":"value"}

Response: [POST] api/account/login

    {"UserId":"value", "Username":"value", FirstName":"value", "LastName":"value", "Access_Token":"value"}

----------

> *api/{userId}/account/logout

Description: logging out

Request: [GET] api/{userId}/account/logout

Body: Empty

Response: [GET] api/{userId}/account/logout

    Empty (only status code)


----------


> api/account/signup

Description: This method register user in the system

Request: [POST] api/account/signup

Body:

    {"Username":"value", "Password":"value","FirstName":"value", "LastName":"value"}

Response: [POST] api/account/signup

    Empty (only status code)


----------


> *api/{userId}/account/changeuserinfo

Description: Change user first name, last name and username

Request: [POST] api/{userId}/account/logout

Body:

    {"Username":"value", FirstName":"value", "LastName":"value"}

Response: [POST] api/{userId}/account/logout

    {"UserId":"value", "Username":"value", FirstName":"value", "LastName":"value"}


----------


> *api/{userId}/account/changepass

Description: Change user password

Request: [POST] api/{userId}/account/logout

Body:

    {"OldPassword":"value", NewPassword":"value"}

Response: [POST] api/{userId}/account/changepass

    Empty (or error message)


----------


> *api/getimg/{filename}

Description: Return user image or default image

Request: [GET] api/getimg/{filename} (filename: "{userid}.jpg")

Body: 

    Empty

Response: [GET] api/getimg/{filename}

    raw data (bytes of image)


----------


> api/{userId}/setimg?filename={userid}.jpg

Description: Set image for user

Request: [POST] api/{userId}/setimg

Body:

    raw data (bytes of image)

Response: [POST] api/{userId}/setimg

    Empty (or error message)

## Social Services API

> api/{userId}/services/vk/addservice?code={value}

Description: Add vk.com service for user into services list. Code is parameter returned from vk.com on authorization process

Request: [GET] api/{userId}/services/vk/addservice

Response: [GET] api/{userId}/services/vk/addservice

    Empty (or error message)

----------

> api/{userId}/services/vk/search?query={value}

Description: Search people from user’s vk.com friends list

Request: [GET] api/{userId}/services/vk/search

Response: [GET] api/{userId}/services/vk/search

    [{"UserId":"value", "Username":"value","FirstName":"value", "LastName":"value", "Availability":"value", "AuthorizationId":"value", "PhotoUri":"value"}, {…}, …]
     
----------


> api/{userId}/services/vk/check

Description: Check for availability of vk.com account

Request: [GET] api/{userId}/services/vk/check

Response: [GET] api/{userId}/services/vk/check

    "true"/"false" or error message

  


----------


  

> api/{userId}/services/email/sendinvent?email={value}&signup_url={value}

Description: Send email invitation to join SafeMoney. Email parameter is email:) signup_url is url to signup page or app store link

Request: [GET] api/{userId}/services/email/sendinvent

Response: [GET] api/{userId}/services/email/sendinvent

    Empty (or error message)

## Transactions API

> api/{userId}/transactions/getuserlist

Description: Return full friend list for user

Request: [GET] api/{userId}/transactions/getuserlist

Response: [GET] api/{userId}/transactions/getuserlist

    [{"UserId":"value", "Username":"value","FirstName":"value", "LastName":"value"}, {…}, …]


----------


> api/{userId}/transactions/getuserlist?search={value}

Description: Return result of searching.

Request: [GET] api/{userId}/transactions/getuserlist

Response: [GET] api/{userId}/transactions/getuserlist

    [{"UserId":"value", "Username":"value","FirstName":"value", "LastName":"value"}, {…}, …]

  
  

  


----------


  

> api/{userId}/transactions/add

Description: Add new not permitted transaction

Request: [POST] api/{userId}/transactions/add

Body:

    {"userId":"value", "count":"value", "date":"value", "period":"value", "comment":"value"}

Response: [PSOT] api/{userId}/transactions/add

    Empty (or error message)

  


----------


  

> api/{userId}/transactions/checkqueue

Description: Check transactions table and retern not permited for this user

Request: [GET] api/{userId}/transactions/checkqueue

Response: [GET] api/{userId}/transactions/checkqueue

    [{
    
    "transactionData":{
    
    "Id":"value","User1Id":"value","User2Id":"value","Count":"value", "Date":"value", "Period":"value", "IsPermited":"value", "IsClosed":"value", "Comment":"value"},
    
    "userData":{
    
    "UserId":"value","Username":"value","FirstName":"value","LastName":"value", "PhotoUri":"value:"value"}
    
    },{…}, …]

  
  


----------


> api/{userId}/transactions/confirm/{transId}

Description: Confirm transaction

Request: [GET] api/{userId}/transactions/confirm/{transId}

Response: [GET] api/{userId}/transactions/confirm/{transId}

    Empty (or error message)


----------


> api/{userId}/transactions/close/{transId}

Description: Close transaction

Request: [GET] api/{userId}/transactions/close/{transId}

Response: [GET] api/{userId}/transactions/close/{transId}

    Empty (or error message)

  


----------


  

> api/{userId}/transactions/fetch

Description: Return all unclosed transactions for user

Request: [GET] api/{userId}/transactions/fetch

Response: [GET] api/{userId}/transactions/fetch

    [{
    
    "transactionData":{
    
    "Id":"value","User1Id":"value","User2Id":"value","Count":"value", "Date":"value", "Period":"value", "IsPermited":"value", "IsClosed":"value", "Comment":"value"},
    
    "userData":{
    
    "UserId":"value","Username":"value","FirstName":"value","LastName":"value", "PhotoUri":"value:"value"}
    
    },{…}, …]


## Payments API
Coming soon
