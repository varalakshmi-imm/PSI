namespace PSI;

using System.Xml.Linq;

// An basic XML code generator, implemented using the Visitor pattern
public class ExprXMLGen : Visitor<XElement> {

   public override XElement Visit (NLiteral literal)
      => new ("Literal", new XAttribute ("Value", literal.Value.Text)
                       , new XAttribute ("Type", literal.Type.ToString ()));

   public override XElement Visit (NIdentifier identifier)
      => new ("Ident", new XAttribute ("Name", identifier.Name)
                     , new XAttribute ("Type", identifier.Type.ToString ()));

   public override XElement Visit (NUnary unary) {
      var a = unary.Expr.Accept (this);
      XElement elem =  new ("Unary", new XAttribute ("Op", unary.Op.Kind.ToString ())
                         , new XAttribute ("Type", unary.Type.ToString ()));
      elem.Add (a);
      return elem;
   }

   public override XElement Visit (NBinary binary) {
      var a= binary.Left.Accept (this); var b = binary.Right.Accept (this);
      XElement elem = new ("Binary", new XAttribute ("Op", binary.Op.Kind.ToString ())
                          , new XAttribute ("Type", binary.Type.ToString ()));
      elem.Add (a); elem.Add (b);
      return elem;
   }
}