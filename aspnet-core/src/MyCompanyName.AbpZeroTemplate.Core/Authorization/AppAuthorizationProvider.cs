using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MyCompanyName.AbpZeroTemplate.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var pbOppinions = pages.CreateChildPermission(AppPermissions.Pages_PbOppinions, L("PbOppinions"));
            pbOppinions.CreateChildPermission(AppPermissions.Pages_PbOppinions_Create, L("CreateNewPbOppinion"));
            pbOppinions.CreateChildPermission(AppPermissions.Pages_PbOppinions_Edit, L("EditPbOppinion"));
            pbOppinions.CreateChildPermission(AppPermissions.Pages_PbOppinions_Delete, L("DeletePbOppinion"));



            var pbEbooks = pages.CreateChildPermission(AppPermissions.Pages_PbEbooks, L("PbEbooks"));
            pbEbooks.CreateChildPermission(AppPermissions.Pages_PbEbooks_Create, L("CreateNewPbEbook"));
            pbEbooks.CreateChildPermission(AppPermissions.Pages_PbEbooks_Edit, L("EditPbEbook"));
            pbEbooks.CreateChildPermission(AppPermissions.Pages_PbEbooks_Delete, L("DeletePbEbook"));



            var pbSubjects = pages.CreateChildPermission(AppPermissions.Pages_PbSubjects, L("PbSubjects"));
            pbSubjects.CreateChildPermission(AppPermissions.Pages_PbSubjects_Create, L("CreateNewPbSubject"));
            pbSubjects.CreateChildPermission(AppPermissions.Pages_PbSubjects_Edit, L("EditPbSubject"));
            pbSubjects.CreateChildPermission(AppPermissions.Pages_PbSubjects_Delete, L("DeletePbSubject"));


            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var pbTypeEbooks = administration.CreateChildPermission(AppPermissions.Pages_Administration_PbTypeEbooks, L("PbTypeEbooks"));
            pbTypeEbooks.CreateChildPermission(AppPermissions.Pages_Administration_PbTypeEbooks_Create, L("CreateNewPbTypeEbook"));
            pbTypeEbooks.CreateChildPermission(AppPermissions.Pages_Administration_PbTypeEbooks_Edit, L("EditPbTypeEbook"));
            pbTypeEbooks.CreateChildPermission(AppPermissions.Pages_Administration_PbTypeEbooks_Delete, L("DeletePbTypeEbook"));



            var pbPlaces = administration.CreateChildPermission(AppPermissions.Pages_Administration_PbPlaces, L("PbPlaces"));
            pbPlaces.CreateChildPermission(AppPermissions.Pages_Administration_PbPlaces_Create, L("CreateNewPbPlace"));
            pbPlaces.CreateChildPermission(AppPermissions.Pages_Administration_PbPlaces_Edit, L("EditPbPlace"));
            pbPlaces.CreateChildPermission(AppPermissions.Pages_Administration_PbPlaces_Delete, L("DeletePbPlace"));



            var pbSubjectEducations = administration.CreateChildPermission(AppPermissions.Pages_Administration_PbSubjectEducations, L("PbSubjectEducations"));
            pbSubjectEducations.CreateChildPermission(AppPermissions.Pages_Administration_PbSubjectEducations_Create, L("CreateNewPbSubjectEducation"));
            pbSubjectEducations.CreateChildPermission(AppPermissions.Pages_Administration_PbSubjectEducations_Edit, L("EditPbSubjectEducation"));
            pbSubjectEducations.CreateChildPermission(AppPermissions.Pages_Administration_PbSubjectEducations_Delete, L("DeletePbSubjectEducation"));



            var pbStatuses = administration.CreateChildPermission(AppPermissions.Pages_Administration_PbStatuses, L("PbStatuses"));
            pbStatuses.CreateChildPermission(AppPermissions.Pages_Administration_PbStatuses_Create, L("CreateNewPbStatus"));
            pbStatuses.CreateChildPermission(AppPermissions.Pages_Administration_PbStatuses_Edit, L("EditPbStatus"));
            pbStatuses.CreateChildPermission(AppPermissions.Pages_Administration_PbStatuses_Delete, L("DeletePbStatus"));



            var pbTypeFiles = administration.CreateChildPermission(AppPermissions.Pages_Administration_PbTypeFiles, L("PbTypeFiles"));
            pbTypeFiles.CreateChildPermission(AppPermissions.Pages_Administration_PbTypeFiles_Create, L("CreateNewPbTypeFile"));
            pbTypeFiles.CreateChildPermission(AppPermissions.Pages_Administration_PbTypeFiles_Edit, L("EditPbTypeFile"));
            pbTypeFiles.CreateChildPermission(AppPermissions.Pages_Administration_PbTypeFiles_Delete, L("DeletePbTypeFile"));



            var pbRanks = administration.CreateChildPermission(AppPermissions.Pages_Administration_PbRanks, L("PbRanks"));
            pbRanks.CreateChildPermission(AppPermissions.Pages_Administration_PbRanks_Create, L("CreateNewPbRank"));
            pbRanks.CreateChildPermission(AppPermissions.Pages_Administration_PbRanks_Edit, L("EditPbRank"));
            pbRanks.CreateChildPermission(AppPermissions.Pages_Administration_PbRanks_Delete, L("DeletePbRank"));



            var pbClasses = administration.CreateChildPermission(AppPermissions.Pages_Administration_PbClasses, L("PbClasses"));
            pbClasses.CreateChildPermission(AppPermissions.Pages_Administration_PbClasses_Create, L("CreateNewPbClass"));
            pbClasses.CreateChildPermission(AppPermissions.Pages_Administration_PbClasses_Edit, L("EditPbClass"));
            pbClasses.CreateChildPermission(AppPermissions.Pages_Administration_PbClasses_Delete, L("DeletePbClass"));



            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host); 

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
