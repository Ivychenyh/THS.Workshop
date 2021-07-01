Feature: 會員管理測試
	身為會員
	我希望系統能提供會員"新刪查改"功能
	以便我能方便管理會員資料

Scenario: 新增一筆會員資料
	Given 前端應傳來以下新增請求資料
		 | Email     | Name | Age |
		 | yao@aa.bb | yao  | 18  |
	When 調用新增
	Then 預期資料庫的 Member 資料表應有以下資料
		 | Email     | Name | Age | 
		 | yao@aa.bb | yao  | 18  | 

Scenario: 編輯一筆會員資料
	Given 資料庫Member已存在以下資料
		 | Email	 | Name | Age |
		 | yao@aa.bb | yao  | 18  | 
	Given 前端應傳來以下編輯請求資料
		 | Email	  | Name  | Age |
		 | yao@aa.bb1 | yao1  | 19  |
	When 調用編輯
	Then 預期資料庫的 Member 資料表應有以下資料
		 | Email      | Name | Age | 
		 | yao@aa.bb1 | yao1 | 19  | 	
		 
Scenario: 刪除一筆會員資料
	Given 資料庫Member已存在以下資料
		| ID | Email | Name | Age |
		|1	 | yao@aa.bb | yao  | 18  | 
	Given 前端應傳來以下刪除請求資料
		| ID |
		|1	 |
	When 調用刪除
	Then 預期資料庫的 Member 資料表應有以下資料
		 | Email     | Name | Age | 

Scenario: 查詢會員資料
	Given 資料庫Member已存在以下資料
		| Id | Email      | Name | Age |
		| 1  | yao@aa.bb1 | yao1 | 18  |
		| 2  | yao@aa.bb2 | yao2 | 18  |
		| 3  | yao@aa.bb3 | yao3 | 18  |
		| 4  | yao@aa.bb4 | yao4 | 18  |
		| 5  | yao@aa.bb5 | yao5 | 18  |
	And 前端應傳來以下查詢請求資料
		 | Email	  | Name | Age |
		 | yao@aa.bb1 | yao1 | 18  | 
	And 前端應穿來以下GridState資料
		| PageSize | PageIndex |
		| 10       | 0         |
	When 調用查詢
	Then 預期查詢結果有以下資料
	| ID | Email	 | Name  | Age |
	| 1	 | yao@aa.bb1| yao1  | 18  | 
