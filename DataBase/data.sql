-- 插入 PolicyHolder 表的資料，並使用 NEWID() 作為 code 和 introducer_code 的值
INSERT INTO sample..PolicyHolder (code, name, registration_date, introducer_code)
VALUES
    ('A123456789','John', '2023-01-01', 'A123456780'),
    ('B112243345','David', '2023-01-02', 'A123456789'),
    ('C233445564','Mary', '2023-01-03', 'A123456789'),
    ('D123456789','Emma', '2023-01-04', 'A123456789'),
    ('E112233445','Alice', '2023-01-05', 'A123456789'),
    ('F233445564','Tom', '2023-01-06','A123456789'),
    ('G123456789','Michael', '2023-01-07', 'B112243345'),
    ('H112433445','Sophia', '2023-01-08', 'B112243345'),
    ('I233445564','Olivia', '2023-01-09', 'B112243345'),
    ('J123456789','Emily', '2023-01-10', 'B112243345');
	
INSERT INTO sample..PolicyHolder_BinaryTree(id , left_child_id , policyholder_code , right_child_id)
VALUES
    (1 , NULL, 'A123456789', NULL),
    (2 , NULL, 'B112243345', NULL),
    (3 , NULL, 'C233445564', NULL),
    (4 , NULL, 'D123456789', NULL),
    (5 , NULL, 'E112233445', NULL),
    (6 , NULL, 'F233445564', NULL),
    (7 , NULL, 'G123456789', NULL),
    (8 , NULL, 'H112433445', NULL),
    (9 , NULL, 'I233445564', NULL),
    (10 , NULL, 'J123456789', NULL);

UPDATE sample..PolicyHolder_BinaryTree
SET left_child_id = 2 , right_child_id = 3
WHERE policyholder_code = 'A123456789'

UPDATE sample..PolicyHolder_BinaryTree
SET left_child_id = 4 , right_child_id = 5
WHERE policyholder_code = 'B112243345'

UPDATE sample..PolicyHolder_BinaryTree
SET left_child_id = 6 , right_child_id = 7
WHERE policyholder_code = 'C233445564'

UPDATE sample..PolicyHolder_BinaryTree
SET left_child_id = 8 , right_child_id = 9
WHERE policyholder_code = 'D123456789'

UPDATE sample..PolicyHolder_BinaryTree
SET left_child_id = 10
WHERE policyholder_code = 'E112233445'