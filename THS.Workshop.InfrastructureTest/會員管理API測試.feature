Feature: 會員管理API測試
	調用WebApi完成會員增刪查改功能

@mytag
Scenario: 新增一筆會員資料
	Given 前端應傳來以下新增請求資料
		 | Email     | Name | Age |
		 | yao@aa.bb | yao  | 18  |
	When 調用Post新增會員資料
	Then 預期HttpStatusCode為200
	And 預期資料庫的 Member 資料表應有以下資料
		 | Email     | Name | Age | 
		 | yao@aa.bb | yao  | 18  | 