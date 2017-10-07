/*
	Конвертер изображений в текстовый файл формата ASCII.
*/
#include <string>
#include "EasyBMP.h"

char * chooseChar(int value)
{ // Выбор символа для вывода 
  // 255 - белый, 0 - чёрный
	if (value>192)					
		return " ";
	else if (value>128)
		return ".";
	else if (value>64)
		return "+";
	else
		return "#";
}
void bmp2txt (BMP image, FILE * fpout)
{ // Попиксельный перевод bmp в 4-хсимвольный ASCII
	for (int j=1; j<image.TellHeight(); ++j)
	 {
		for (int i=1; i<image.TellWidth(); ++i)
			{
				int R = image(i,j)->Red,
					G = image(i,j)->Green,
					B = image(i,j)->Blue,
					m = (R+G+B)/3; // среднее значение пискеля по RGB
				fputs (chooseChar(m), fpout); // вывод соотв. символа в файл
			}
		fputs ("\n", fpout);
	 }
}

int main()
{
	// Изображение и текстовый файл-результат
	BMP MyImage;
	FILE *fpout=fopen ("result.txt", "w");

	// Меню для выбора (исходный пример или собственное изображение)
	std::string str="digdes.bmp";
	std::cout<<">1 use example picture\n (it should be in the same directory as exe)\n>2 use other picture\n-> ";
	getline(std::cin, str);
	if (str=="1") // пример
		str="digdes.bmp";
	else if (str=="2")
	{	// свое изображение с дирикторией
		std::cout<<"Enter full name (with directory) -> "; 	
		getline(std::cin, str);
	}
	
	const char * file_name = str.c_str(); // преобр. имени файла
	
	// открытие изображения
	if ( ! MyImage.ReadFromFile(file_name) )
	{
		std::cout<<"Cannot convert non-bmp file"<<std::endl;
		fputs ("Failed.", fpout);
	}
	else
	{
		bmp2txt(MyImage, fpout); // конвертирование
		std::cout<<"Finished. Check exe directory for result.txt"<<std::endl;
	}

	fclose(fpout);
	system("PAUSE");
	return 0;
}