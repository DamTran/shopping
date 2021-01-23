SELECT m.Id
	, m.CategoryId
	, m.Description
	, m.FoodTypeId
	, m.Image
	, m.Name
	, m.Price
	, c.Id
	, c.DisplayOrder
	, c.Name
	, f.Id
	, f.Name
FROM MenuItem AS m
	INNER JOIN Category AS c ON m.CategoryId = c.Id
	INNER JOIN FoodType AS f ON m.FoodTypeId = f.Id