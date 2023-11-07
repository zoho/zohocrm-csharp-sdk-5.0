————————————————————————
Zoho CRM C# SDK 2.0.0
————————————————————————

This is the readme file for Zoho CRM’s C# SDK version 2.0.0.

This file gives a brief of the enhancements and/or bug fixes in the latest version.

----------------
Enhancements
----------------
- Added new column(api_domain) in TokenStore.
- Added a new key FindUser in OAuthToken.
    - By default, the FindUser key is set to true, to set userSignature. However, this requires the ***ZohoCRM.users.READ*** and ***ZohoCRM.org.READ*** scopes to be mandatory. If you do not want to set UserSignature, you must set the FindUser key to false.
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

You can also take a look at our GitHub page here (https://github.com/zoho/zohocrm-csharp-sdk-5.0/blob/master/README.md)