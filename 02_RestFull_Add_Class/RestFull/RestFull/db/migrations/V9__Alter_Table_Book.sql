ALTER TABLE `books`
	ADD COLUMN `available` BIT(1) NOT NULL DEFAULT b'1' AFTER `launch_date`;