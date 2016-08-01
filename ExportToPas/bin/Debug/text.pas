{Layer: Background}
Image1.Canvas.Brush.Color:=$FFFFFF;
Image1.Canvas.FillRect(Image1.ClientRect);
{Text 0}
Image1.Canvas.Brush.Style:=bsClear;
Image1.Canvas.Font.Height:=16;
Image1.Canvas.Font.Color:=RGBToColor(238, 44, 44);
Image1.Canvas.Font.Name:='Aharoni';
Image1.Canvas.Font.Style:=[fsBold, fsItalic, fsUnderline];
Image1.Canvas.TextOut(248, 154, 'Hello');
