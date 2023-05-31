program Comp10;
var 
  n: integer;
  
function D2R (deg: real): real;
begin
   D2R := deg * Pi / 180;
end;

begin
  n := 45;
  WriteLn (n, " degrees = ", D2R (n), " radians.");
  WriteLn ("Sin (", n, ") = ", Sin (D2R (n)));
  WriteLn ("Sqrt (2) / 2 = ", Sqrt (2) / 2);
end.