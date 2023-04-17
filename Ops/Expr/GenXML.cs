namespace PSI;

using System.Xml.Linq;

// An basic XML code generator, implemented using the Visitor pattern
public class ExprXMLGen : Visitor<XElement> {

   public override XElement Visit (NLiteral literal)
      => New ("Literal", ("Value", literal.Value.Text), ("Type", literal.Type));

   public override XElement Visit (NIdentifier identifier)
      => New ("Ident", ("Name", identifier.Name.Text), ("Type", identifier.Type));

   public override XElement Visit (NUnary unary)
      => New ("Unary", ("Op", unary.Op.Kind), ("Type", unary.Type), unary.Expr.Accept (this));

   public override XElement Visit (NBinary binary)
      => New ("Binary", ("Op", binary.Op.Kind), ("Type", binary.Type)
                      , binary.Left.Accept (this), binary.Right.Accept (this));

   XElement New (string name, params object[] data) {
      var node = new XElement (name);
      foreach (var d in data) {
         if (d is (string K, object V)) node.SetAttributeValue (K, V);
         else if (d is XElement x) node.Add (d);
         else throw new NotImplementedException ();
      }
      return node;
   }
}