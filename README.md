# ZOHO CRM CSHARP SDK 5.0 for API version 5

The C# SDK for Zoho CRM allows developers to easily create C# applications that can be integrated with Zoho CRM. This SDK serves as a wrapper for the REST APIs, making it easier to access and utilize the services of Zoho CRM. 
Authentication to access the CRM APIs is done through OAuth2.0, and the authentication process is streamlined through the use of the C# SDK. The grant and access/refresh tokens are generated and managed within the SDK code, eliminating the need for manual handling during data synchronization between Zoho CRM and the client application.

This repository includes the C# SDK for API v5 of Zoho CRM. Check [Versions](https://github.com/zoho/zohocrm-csharp-sdk-5.0/releases) for more details on the versions of SDK released for this API version.

License
=======

    Copyright (c) 2021, ZOHO CORPORATION PRIVATE LIMITED 
    All rights reserved. 

    Licensed under the Apache License, Version 2.0 (the "License"); 
    you may not use this file except in compliance with the License. 
    You may obtain a copy of the License at 
    
        http://www.apache.org/licenses/LICENSE-2.0 
    
    Unless required by applicable law or agreed to in writing, software 
    distributed under the License is distributed on an "AS IS" BASIS, 
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
    See the License for the specific language governing permissions and 
    limitations under the License.

## Latest Version

- [2.0.0](/versions/2.0.0/README.md)
    - Added new column(api_domain) in TokenStore.
    - Added a new key FindUser in OAuthToken.
        - By default, the FindUser key is set to true, to set UserSignature. However, this requires the ***ZohoCRM.users.READ*** and ***ZohoCRM.org.READ*** scopes to be mandatory. If you do not want to set UserSignature, you must set the FindUser key to false.
    - IDS param datatype changed(Long to String).
        - GetAttachmentsParam
        - DeleteAttachmentsParam
        - DeleteRolesParam
        - GetAssociatedContactRolesParam
        - DeleteNotesParam
        - DeleteScoringRulesParam
        - DeleteTerritoriesParam
        - DeassociateTerritoryUsersParam
        - DeleteVariablesParam
    - Note class seModule field datatype changed(Choice<String> to String).
    - If-Modified-Since param datatype changed (String to OffsetDateTime).
        - GetNotesHeader
        - GetNoteHeader 
    - Support for the following new APIs.
        - Organization:
            - [Get Organization Photo](https://www.zoho.com/crm/developer/docs/api/v5/get-org-img.html)
            - [Delete Organization Photo](https://www.zoho.com/crm/developer/docs/api/v5/delete-org-img.html)
        - Record Locking:
            - [Record Locking Information APIs](https://www.zoho.com/crm/developer/docs/api/v5/get-record-locking-info.html)
            - [Lock Records](https://www.zoho.com/crm/developer/docs/api/v5/lock-records.html)
            - [Update Record Locking Information](https://www.zoho.com/crm/developer/docs/api/v5/update-record-locking-info.html)
            - [Unlock Records](https://www.zoho.com/crm/developer/docs/api/v5/unlock-records.html)
    - RescheduleHistory ResponseWrapper info field datatype changed(List<into> to info).
    - ScoringRules Signal namespace field datatype changed(Choice<String> to String).
    - Tags RecordActionWrapper lockedCount field datatype changed(Boolean to String).
    - UsersTerritories Territory id field datatype changed(Long to String).
    - VariablesOperations updateVariableByApiname method add new ParameterMap param.

- [1.0.0](/versions/1.0.0/README.md)

    - C# SDK upgraded to support v5 APIs.

    - C# SDK improved to support the following new APIs

        - [User Groups API](https://www.zoho.com/crm/developer/docs/api/v5/associated-user-count-user-group.html)
        - [Fiscal Years](https://www.zoho.com/crm/developer/docs/api/v5/get-fiscal-year.html)
        - [Timeline API](https://www.zoho.com/crm/developer/docs/api/v5/timeline-of-a-record.html)
        - [Transfer and Delete Users](https://www.zoho.com/crm/developer/docs/api/v5/transfer_records-delete_user.html)
        - [Territories](https://www.zoho.com/crm/developer/docs/api/v5/add-territories.html)
        - [Territories Users](https://www.zoho.com/crm/developer/docs/api/v5/associate-users-territory.html)     


For older versions, please [refer](https://github.com/zoho/zohocrm-csharp-sdk-5.0/releases).

## Environmental Setup

C# SDK requires .NET Framework 4.6.1 or above to be set up in your development environment.

## Including the SDK in your project

You can include the SDK to your project using:

1. Install Visual Studio IDE from [Visual Studio](https://visualstudio.microsoft.com/downloads/) (if not installed).

2. C# SDK is available as a Nuget package. The ZOHOCRMSDK-5.0 assembly can be installed through the Nuget Package Manager or through the following options:

    - Package Manager

        ```sh
        Install-Package ZOHOCRMSDK-5.0 -Version 2.0.0
        Install-Package MySql.Data -Version 6.9.12
        Install-Package Newtonsoft.Json -Version 13.0.1
        ```

    - .NET  CLI

        ```sh
        dotnet add package ZOHOCRMSDK-5.0 --version 2.0.0
        dotnet add package Newtonsoft.Json --version 13.0.1
        dotnet add package MySql.Data --version 6.9.12
        ```

    - PackageReference

        For projects that support PackageReference, copy this XML node into the project file to refer the package.

        ```sh
        <ItemGroup>
            <PackageReference Include="ZOHOCRMSDK-5.0" Version="2.0.0" />
            <PackageReference Include="Newtonsoft.Json" Version="13.0.1" />
            <PackageReference Include="MySql.Data" Version="6.9.12" />
        </ItemGroup>
        ```
---

**NOTE** 

> - The **access and refresh tokens are environment-specific and domain-specific**. When you handle various environments and domains such as **Production**, **Sandbox**, or **Developer** and **IN**, **CN**, **US**, **EU**, **JP**, or **AU**, respectively, you must use the access token and refresh token generated only in those respective environments and domains. The SDK throws an error, otherwise.
For example, if you generate the tokens for your Sandbox environment in the CN domain, you must use only those tokens for that domain and environment. You cannot use the tokens generated for a different environment or a domain.

> - For **Deal Contact Roles API and Records API**, you will need to provide the **ZohoCRM.settings.fields.ALL** scope along with the **ZohoCRM.modules.ALL** scope while generating the OAuthtoken. Otherwise, the system returns the **OAUTH-SCOPE-MISMATCH** error.

> - For **Related Records API**, the scopes required for generating OAuthtoken are **ZohoCRM.modules.ALL**, **ZohoCRM.settings.fields.ALL** and **ZohoCRM.settings.related_lists.ALL**. Otherwise, the system returns the **OAUTH-SCOPE-MISMATCH** error.

> - For **Mass Convert API**, you will need to provide the **ZohoCRM.settings.fields.ALL** scope along with the **ZohoCRM.mass_convert.leads.CREATE** and **ZohoCRM.mass_convert.leads.READ** scope while generating the OAuthtoken. Otherwise, the system returns the **OAUTH-SCOPE-MISMATCH** error.

---

For more details, kindly refer [here](/versions/2.0.0/README.md).