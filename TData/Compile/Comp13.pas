program Comp13;
var 
  g: integer;

function Div (a, b: integer) : integer;
var
  result, res2: integer;
begin
  result := a * b;
  res2 := a / b;
  Div := res2;
end
  
begin
  g := Div (13, 5);
  WriteLn ("13 / 5 = ", g);
end.
