﻿using Syncfusion.Presentation;

//Load or open an PowerPoint Presentation.
using FileStream inputStream = new(Path.GetFullPath(@"Data/Template.pptx"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
//Open an existing PowerPoint presentation.
using IPresentation pptxDoc = Presentation.Open(inputStream);
//Get slide from the Presentation.
ISlide slide = pptxDoc.Slides[0];
//Get the table from slide.
ITable table = slide.Shapes[0] as ITable;
//Remove table from shape collection.
slide.Shapes.Remove(table);
using FileStream outputStream = new(Path.GetFullPath(@"Output/Result.pptx"), FileMode.Create, FileAccess.ReadWrite);
pptxDoc.Save(outputStream);