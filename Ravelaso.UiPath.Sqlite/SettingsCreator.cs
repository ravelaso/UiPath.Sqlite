// using UiPath.Studio.Activities.Api;
// using UiPath.Studio.Activities.Api.Settings;
// using UiPath.Studio.Api;
//
// namespace Ravelaso.UiPath.Sqlite;
//
// public static class SettingsCreator
// {
//     internal const string CategoryKey = "DemoTabUniqueKey";
//     internal const string CommentTextKey = CategoryKey + ".CommentText";
//     internal const string PresetKey = CategoryKey + ".Preset";
//     internal const string DoSomething = CategoryKey + ".DoSomething";
//
//     public static void CreateSettings(IWorkflowDesignApi workflowDesignApi)
//     {
//         var settingsApi = workflowDesignApi.Settings;
//         var category = new SettingsCategory()
//         {
//             Key = CategoryKey,
//             Description = "Settings Description",
//             Header = "Settings Header"
//         };
//         settingsApi.AddCategory(category);
//         // 
//     }
//
//     private static SettingsSection AddSqliteSection(IActivitiesSettingsService settingsApi,
//         SettingsCategory category)
//     {
//         var section = new SettingsSection()
//         {
//             Description = "Settings for Sqlite",
//             IsExpanded = true,
//             Title = "Sqlite Settings",
//             Key = "Sqlite"
//         };
//         settingsApi.AddSection(category, section);
//         // 
//         return section;
//     }
//
//     private static void AddSimpleBoolean(IActivitiesSettingsService settingsApi, SettingsSection section)
//     {
//         var booleanSetting = new SingleValueEditorDescription<bool>
//         {
//             DefaultValue = true,
//             Description = "If true, well is active",
//             GetDisplayValue = b => (b ? "Yes" : "No"),
//             Key = DoSomething,
//             Label = "Do Something"
//         };
//         settingsApi.AddSetting(section, booleanSetting);
//     }
//
//     private static void AddSingleChoice(IActivitiesSettingsService settingsApi, SettingsSection category)
//     {
//         var booleanSetting = new SingleValueSelectorDescription
//         {
//             DefaultValue = "pop",
//             Values = new[] { "classic", "pop", "rock" },
//             Description = "Sample single choice setting",
//             // The value returned by GetDisplayValue should be localized
//             GetDisplayValue = choice => choice + " music",
//             Key = PresetKey,
//             Label = "Mixer Preset"
//         };
//         settingsApi.AddSetting(category, booleanSetting);
//     }
//     private static void AddSimpleString(IActivitiesSettingsService settingsApi, SettingsSection section)
//     {
//         var simpleStringSetting = new SingleValueEditorDescription<string>()
//         {
//             Description = "A free text comment that can't contain the word 'invalid'",
//             // The GetDisplayValue obtains a localized screen representation of the underlying setting value.
//             GetDisplayValue = LocalizeSimpleSettingValue,
//             IsReadOnly = false,
//             DefaultValue = "There is no comment",
//             Label = "Comment",
//             Validate = ValidateSimpleStringSetting,
//             Key = CommentTextKey
//         };
//         // Add the setting to the section.
//         // A setting may also be directly added on a category. It will appear as a setting without a section (top level setting)
//         settingsApi.AddSetting(section, simpleStringSetting);
//     }
//     private static string LocalizeSimpleSettingValue(string s) => $"A localized value of <code>{s}</code>";
//     private static string ValidateSimpleStringSetting(string arg)
//     {
//         if (arg?.ToLowerInvariant().Contains("invalid") == true)
//         {
//             return "The sample string setting is invalid if it contains the <code>invalid</code> keyword";
//         }
//         return string.Empty;
//     }
//     
// }