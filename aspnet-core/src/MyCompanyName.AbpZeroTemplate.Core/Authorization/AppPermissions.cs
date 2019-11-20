namespace MyCompanyName.AbpZeroTemplate.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        public const string Pages_PbDownloadEbooks = "Pages.PbDownloadEbooks";
        public const string Pages_PbDownloadEbooks_Create = "Pages.PbDownloadEbooks.Create";
        public const string Pages_PbDownloadEbooks_Edit = "Pages.PbDownloadEbooks.Edit";
        public const string Pages_PbDownloadEbooks_Delete = "Pages.PbDownloadEbooks.Delete";

        public const string Pages_PbOppinions = "Pages.PbOppinions";
        public const string Pages_PbOppinions_Create = "Pages.PbOppinions.Create";
        public const string Pages_PbOppinions_Edit = "Pages.PbOppinions.Edit";
        public const string Pages_PbOppinions_Delete = "Pages.PbOppinions.Delete";

        public const string Pages_PbEbooks = "Pages.PbEbooks";
        public const string Pages_PbEbooks_Create = "Pages.PbEbooks.Create";
        public const string Pages_PbEbooks_Edit = "Pages.PbEbooks.Edit";
        public const string Pages_PbEbooks_Delete = "Pages.PbEbooks.Delete";

        public const string Pages_Administration_PbTypeEbooks = "Pages.Administration.PbTypeEbooks";
        public const string Pages_Administration_PbTypeEbooks_Create = "Pages.Administration.PbTypeEbooks.Create";
        public const string Pages_Administration_PbTypeEbooks_Edit = "Pages.Administration.PbTypeEbooks.Edit";
        public const string Pages_Administration_PbTypeEbooks_Delete = "Pages.Administration.PbTypeEbooks.Delete";

        public const string Pages_Administration_PbPlaces = "Pages.Administration.PbPlaces";
        public const string Pages_Administration_PbPlaces_Create = "Pages.Administration.PbPlaces.Create";
        public const string Pages_Administration_PbPlaces_Edit = "Pages.Administration.PbPlaces.Edit";
        public const string Pages_Administration_PbPlaces_Delete = "Pages.Administration.PbPlaces.Delete";

        public const string Pages_Administration_PbSubjectEducations = "Pages.Administration.PbSubjectEducations";
        public const string Pages_Administration_PbSubjectEducations_Create = "Pages.Administration.PbSubjectEducations.Create";
        public const string Pages_Administration_PbSubjectEducations_Edit = "Pages.Administration.PbSubjectEducations.Edit";
        public const string Pages_Administration_PbSubjectEducations_Delete = "Pages.Administration.PbSubjectEducations.Delete";

        public const string Pages_Administration_PbStatuses = "Pages.Administration.PbStatuses";
        public const string Pages_Administration_PbStatuses_Create = "Pages.Administration.PbStatuses.Create";
        public const string Pages_Administration_PbStatuses_Edit = "Pages.Administration.PbStatuses.Edit";
        public const string Pages_Administration_PbStatuses_Delete = "Pages.Administration.PbStatuses.Delete";

        public const string Pages_Administration_PbTypeFiles = "Pages.Administration.PbTypeFiles";
        public const string Pages_Administration_PbTypeFiles_Create = "Pages.Administration.PbTypeFiles.Create";
        public const string Pages_Administration_PbTypeFiles_Edit = "Pages.Administration.PbTypeFiles.Edit";
        public const string Pages_Administration_PbTypeFiles_Delete = "Pages.Administration.PbTypeFiles.Delete";

        public const string Pages_Administration_PbRanks = "Pages.Administration.PbRanks";
        public const string Pages_Administration_PbRanks_Create = "Pages.Administration.PbRanks.Create";
        public const string Pages_Administration_PbRanks_Edit = "Pages.Administration.PbRanks.Edit";
        public const string Pages_Administration_PbRanks_Delete = "Pages.Administration.PbRanks.Delete";

        public const string Pages_Administration_PbClasses = "Pages.Administration.PbClasses";
        public const string Pages_Administration_PbClasses_Create = "Pages.Administration.PbClasses.Create";
        public const string Pages_Administration_PbClasses_Edit = "Pages.Administration.PbClasses.Edit";
        public const string Pages_Administration_PbClasses_Delete = "Pages.Administration.PbClasses.Delete";

        public const string Pages_PbSubjects = "Pages.PbSubjects";
        public const string Pages_PbSubjects_Create = "Pages.PbSubjects.Create";
        public const string Pages_PbSubjects_Edit = "Pages.PbSubjects.Edit";
        public const string Pages_PbSubjects_Delete = "Pages.PbSubjects.Delete";

        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents= "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";
        public const string Pages_Administration_Users_Unlock = "Pages.Administration.Users.Unlock";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";
        public const string Pages_Administration_OrganizationUnits_ManageRoles = "Pages.Administration.OrganizationUnits.ManageRoles";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        public const string Pages_Administration_UiCustomization = "Pages.Administration.UiCustomization";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";
        public const string Pages_Editions_MoveTenantsToAnotherEdition = "Pages.Editions.MoveTenantsToAnotherEdition";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";

    }
}