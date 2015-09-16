			var bitmap = new Bitmap(bitX, bitY);

			var pixelsForChange = new List<List<PointClass>>();

			#region Generating pixels
			for (var i = 0; i < size; i++)
			{
				pixelsForChange.Add(new List<PointClass>());

				var tempY = i / bitX;
				var tempX = i % bitX;

				for (var y = 0; y <= tempY; y++)
				{
					if (tempY == 0)
					{
						for (var x = 0; x <= tempX; x++)
						{
							pixelsForChange[i].Add(new PointClass(x, y));
						}
					}
					else
					{
						if (y == tempY)
						{
							for (var x = 0; x <= tempX; x++)
							{
								pixelsForChange[i].Add(new PointClass(x, y));
							}
						}
						else
						{
							for (var x = 0; x < bitX; x++)
							{
								pixelsForChange[i].Add(new PointClass(x, y));
							}
						}
					}
				}
			}
			#endregion