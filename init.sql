-- Jadval yaratish
CREATE TABLE IF NOT EXISTS customer(
    id BIGSERIAL PRIMARY KEY,
    balance DECIMAL,
    total_mb DECIMAL NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS package(
    id BIGSERIAL PRIMARY KEY,
    name VARCHAR NOT NULL,
    price DECIMAL NOT NULL,
    traffic_amount_mb DECIMAL NOT NULL
);

CREATE TABLE IF NOT EXISTS status(
    id SERIAL PRIMARY KEY,
    name VARCHAR NOT NULL
);

CREATE TABLE IF NOT EXISTS doc_order(
    id BIGSERIAL PRIMARY KEY,
    customer_id BIGINT NOT NULL REFERENCES customer(id),
    package_id BIGINT NOT NULL REFERENCES package(id),
    status_id INTEGER NOT NULL REFERENCES status(id),
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT now() NOT NULL
);

-- Default ma'lumotlar kiritish
INSERT INTO status (id, name) VALUES
    (1, 'Created'),
    (2, 'Paid'),
    (3, 'Failed')
ON CONFLICT (id) DO NOTHING;

INSERT INTO customer (id, balance, total_mb) VALUES
    (1, 15000.00, 0),
    (2, 32000.50, 0),
    (3, 5000.00, 0)
ON CONFLICT (id) DO NOTHING;

INSERT INTO package (id, name, price, traffic_amount_mb) VALUES
    (1, 'Basic 5GB', 10000.00, 5120),
    (2, 'Standard 15GB', 25000.00, 15360),
    (3, 'Premium 30GB', 40000.00, 30720)
ON CONFLICT (id) DO NOTHING;
