program Comp12;
var 
  g, g2: integer;

function Min3 (a, b: integer): integer;
begin
   Min3 := b;
   if a < b then Min3 := a;
end;

begin
  g := Min3 (5, 13);
  WriteLn ("Min3 (5, 13) = ", g);
  g2 := Min3 (13, 5);
  WriteLn ("Min3 (13, 5) = ", g);
  if 3 < 4 then begin
     WriteLn ("Three is less than four.");
	  WriteLn ("But you know that already.");
  end;
end.