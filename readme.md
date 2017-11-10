# HexaDonut Reversi 
The board game [Reversi](https://en.wikipedia.org/wiki/Reversi) on a hexagonal board with option to play against low-AI or another local player.  
The player with the most pieces on the board at the end of the game wins    
  
![GameGif](https://i.imgur.com/id7I1OD.gif)       
  
## Project Structure  
Logical part - represnting values and rules of the game, board and the players:  
  * LogicBoard.cs -   
  * Index.cs -  
  
Graphical part - store, manage and change images, graphic objects, user interface:
  * GraphicBoard.cs - 
  * PictureBoxItem.cs -  
  * Images.cs -  
  * Menu.cs -  
  
Artificial Intelligence - CPU moves; choosing the best move according to the possible moves and strategy of the game  
  * AlphaBeta.cs -  
  * AlphaBetaBoard.cs -  

## Build with  
[Visual Studio](https://en.wikipedia.org/wiki/Microsoft_Visual_Studio) - The IDE used  
[Windows Forms](https://en.wikipedia.org/wiki/Windows_Forms) - Used for graphical interface
  
## Knowledge 
Written in C#  
The AI is based on:  
[Minimax](https://en.wikipedia.org/wiki/Minimax)  
[Alpha–beta pruning](https://en.wikipedia.org/wiki/Alpha%E2%80%93beta_pruning)  
  
## License 
This project is licensed under the MIT License, please read [license.txt](https://github.com/x5queal/HexaDonut-Reversi/blob/master/license.txt) for more information  

## Todo  
Add setup file & installing info  
