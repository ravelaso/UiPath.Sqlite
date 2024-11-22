using System.Activities.Presentation.Metadata;
using System.ComponentModel;
namespace Ravelaso.UiPath.Sqlite.Design;

public class Metadata: IRegisterMetadata
{
    public void Register()
    {
        var builder = new AttributeTableBuilder();
        builder.AddCustomAttributes(typeof(TestActivity), new DesignerAttribute(typeof(ExecuteQueryDesigner)));
        MetadataStore.AddAttributeTable(builder.CreateTable());
    }
}