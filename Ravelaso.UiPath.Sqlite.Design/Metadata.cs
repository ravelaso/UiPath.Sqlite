using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
namespace Ravelaso.UiPath.Sqlite.Design;

public class Metadata: IRegisterMetadata
{
    public void Register()
{
    var builder = new AttributeTableBuilder();
    builder.ValidateTable();

    var categoryAttribute = new CategoryAttribute($"Ravelaso.UiPath.Sqlite");

    #region NOTE: You need to add 3 lines using the following convention for every activity that you add to the project.
    builder.AddCustomAttributes(typeof(ExecuteQuery), categoryAttribute);
    builder.AddCustomAttributes(typeof(ExecuteQuery), new DesignerAttribute(typeof(ExecuteQueryDesigner)));
    builder.AddCustomAttributes(typeof(ExecuteQuery), new HelpKeywordAttribute(""));

    // builder.AddCustomAttributes(typeof(TestActivity), categoryAttribute);
    // builder.AddCustomAttributes(typeof(TestActivity), new DesignerAttribute(typeof(TestActivityDesigner)));
    // builder.AddCustomAttributes(typeof(TestActivity), new HelpKeywordAttribute(""));

    // builder.AddCustomAttributes(typeof(ScopeActivity), categoryAttribute);
    // builder.AddCustomAttributes(typeof(ScopeActivity), new DesignerAttribute(typeof(ScopeActivityDesigner)));
    // builder.AddCustomAttributes(typeof(ScopeActivity), new HelpKeywordAttribute(""));

    // builder.AddCustomAttributes(typeof(TestScope), categoryAttribute);
    // builder.AddCustomAttributes(typeof(TestScope), new DesignerAttribute(typeof(TestScopeDesigner)));
    // builder.AddCustomAttributes(typeof(TestScope), new HelpKeywordAttribute(""));

    #endregion

    MetadataStore.AddAttributeTable(builder.CreateTable());
}
}