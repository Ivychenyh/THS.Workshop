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

Scenario: 編輯一筆會員資料
	Given 資料庫Member已存在以下資料
		 | Name | Age | Email     |
		 | yao  | 19  | yao@aa.bb |
	Given 前端傳來以下UpdateRequest
		| Name | Age | Email      |
		| yao  | 20  | yao1@aa.bb |
	When 調用Put 'api/members?userId=TEST_USER'
	Then 預期HttpStatusCode為200
	And 預期資料庫的Member資料表有以下資料
		| Name | Age | Email      |
		| yao  | 20  | yao1@aa.bb |

Scenario: 編輯會員資料
	Given 資料庫Member已存在以下資料
		| Id | Name | Age | Email     |
		| 2  | ivy  | 18  | ivy@aa.bb |
	And 前端傳來以下UpdateRequest
		| Id | Name		 | Age | Email      |
		| 2	 | ivy Chen  | 19  | ivy@aa.bb  |
	When 調用Put 'api/members?userId=TEST_USER'
	Then 預期資料庫的Member資料表有以下資料
		| Id | Name		 | Age | Email		 |
		| 2  | ivy Chen  | 19  | ivy@aa.bb	 |

Scenario: 刪除一筆會員資料
	Given 資料庫的Member資料表已存在以下資料
		| Id | Name | Age | Email     |
		| 1  | ivy  | 18  | ivy@aa.bb |
	Given 前端傳來以下DeleteRequest
		| Id | 
		| 1  | 
	When 調用Delete 'api/member/Delete'
	Then 預期HttpStatusCode為200
	And 預期資料庫的Member資料表有以下資料
		| Id | Name | Age | Email     |
