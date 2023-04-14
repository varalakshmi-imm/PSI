namespace PSI;

using System.Xml.Linq;

// An basic XML code generator, implemented using the Visitor pattern
public class ExprXMLGen : Visitor<XElement> {

   public override XElement Visit (NLiteral literal)
      => NewNode ("Literal", ("Value", literal.Value.Text)
                           , ("Type", literal.Type));

   public override XElement Visit (NIdentifier identifier)
      => NewNode ("Ident", ("Name", identifier.Name.Text)
                                  , ("Type", identifier.Type));

   public override XElement Visit (NUnary unary) {
      var a = unary.Expr.Accept (this);
      var elem =  NewNode ("Unary", ("Op", unary.Op.Kind)
                                  , ("Type", unary.Type));
      elem.Add (a);
      return elem;
   }

   public override XElement Visit (NBinary binary) {
      var a= binary.Left.Accept (this); var b = binary.Right.Accept (this);
      var elem = NewNode ("Binary", ("Op", binary.Op.Kind)
                                  , ("Type", binary.Type));
      elem.Add (a); elem.Add (b);
      return elem;
   }

   XElement NewNode (string name, params (string, object)[] attribs)
       => new (name, attribs.Select (a => new XAttribute (a.Item1, a.Item2)));
}