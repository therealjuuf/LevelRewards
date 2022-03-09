# LevelRewards
Reads level rewards from sql table, allocates memory and writes memory pointer to a fixed address on ps_game to be accessed.

# How to Use?
1)Execute "create table.sql"  
2)Execute "usp_Read_LevelRewards_R.sql"  
3)Fill the table  
4)Run the program  
5)Access pointer of the allocated table on ps_game.exe memory at 0x00A00018
