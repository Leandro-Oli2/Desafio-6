
CREATE FUNCTION fn_calcular_valor_total(p_sale_id INT)
RETURNS DECIMAL AS $$
DECLARE 
    total DECIMAL;
BEGIN

    SELECT SUM(sm.quantity * m.price) INTO total
    FROM sales_medicines sm
    JOIN stocks s ON sm.stock_id = s.id
    JOIN medicines m ON s.medicine_id = m.id
    WHERE sm.sale_id = p_sale_id;

    total := COALESCE(total, 0);
	
    UPDATE sales
    SET total_value = total
    WHERE id = p_sale_id;

    RETURN total;
END;
$$ LANGUAGE plpgsql;


-- trigger


CREATE OR REPLACE FUNCTION tr_atualizar_estoque()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE stocks
    SET quantity = quantity - NEW.quantity,
		updated_at = NOW()
    WHERE id = NEW.stock_id;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER tr_atualizar_estoque
BEFORE INSERT ON sales_medicines
FOR EACH ROW
EXECUTE FUNCTION tr_atualizar_estoque();


INSERT INTO stocks (quantity, medicine_id) VALUES (100, 1);


INSERT INTO sales_medicines (quantity, sale_id, stock_id) VALUES (5, 1, 1);


-- view e function

-- Lista de medicamentos com estoque abaixo de um nível mínimo.
CREATE VIEW medicines_out_stock AS
SELECT m.*, COALESCE(s.quantity, 0) AS estoque
FROM medicines m
LEFT JOIN stocks s ON s.medicine_id = m.id
WHERE COALESCE(s.quantity, 0) <= 50;




-- O cliente que mais comprou medicamentos no último mês.
CREATE OR REPLACE FUNCTION fn_top_customer(p_start_date DATE, p_end_date DATE)
RETURNS TABLE (customer VARCHAR, total_purchased BIGINT) AS $$
BEGIN
    RETURN QUERY
    SELECT s.customer, SUM(sm.quantity) AS total_purchased
    FROM sales_medicines sm
    JOIN sales s ON sm.sale_id = s.id
    WHERE s.sale_date BETWEEN p_start_date AND p_end_date
    GROUP BY s.customer
    ORDER BY total_purchased DESC
    LIMIT 1;
END;
$$ LANGUAGE plpgsql;



-- Vendas realizadas em um determinado período.
CREATE FUNCTION fn_get_sales_by_date(p_start_date DATE, p_end_date DATE)
RETURNS TABLE (sale_id INT, customer VARCHAR, sale_date DATE, total_value DECIMAL, user_id INT) AS $$
BEGIN
    RETURN QUERY
    SELECT sales.id AS sale_id, sales.customer, sales.sale_date, sales.total_value, sales.user_id
    FROM sales
    WHERE sales.sale_date BETWEEN p_start_date AND p_end_date;
END;
$$ LANGUAGE plpgsql;