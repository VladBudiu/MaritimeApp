-- ==========================
-- Table: Countries
-- ==========================
CREATE TABLE Countries (
  id SERIAL PRIMARY KEY,
  name VARCHAR NOT NULL UNIQUE
);

-- ==========================
-- Table: Ports
-- ==========================
CREATE TABLE Ports (
  id SERIAL PRIMARY KEY,
  name VARCHAR NOT NULL,
  country_id INTEGER NOT NULL REFERENCES Countries(id),
  UNIQUE(name, country_id)  -- Prevent duplicate port names in same country
);

-- ==========================
-- Table: ShipTypes
-- ==========================
CREATE TABLE ShipTypes (
  id SERIAL PRIMARY KEY,
  type_name VARCHAR NOT NULL UNIQUE
);

-- ==========================
-- Table: Ships
-- ==========================
CREATE TABLE Ships (
  id SERIAL PRIMARY KEY,
  name VARCHAR NOT NULL,
  imo_number VARCHAR(10) NOT NULL UNIQUE,
  ship_type_id INTEGER REFERENCES ShipTypes(id),
  flag_country_id INTEGER REFERENCES Countries(id)
);

-- ==========================
-- Table: Companies
-- ==========================
CREATE TABLE Companies (
  id SERIAL PRIMARY KEY,
  name VARCHAR NOT NULL UNIQUE
);

-- ==========================
-- Table: ShipCompanyRelations
-- ==========================
CREATE TABLE ShipCompanyRelations (
  id SERIAL PRIMARY KEY,
  ship_id INTEGER NOT NULL REFERENCES Ships(id) ON DELETE CASCADE,
  company_id INTEGER NOT NULL REFERENCES Companies(id),
  role VARCHAR NOT NULL CHECK (role IN ('Owner', 'Operator')),
  UNIQUE(ship_id, company_id, role)
);

-- ==========================
-- Table: ShipSpecifications
-- ==========================
CREATE TABLE ShipSpecifications (
  id SERIAL PRIMARY KEY,
  ship_id INTEGER NOT NULL REFERENCES Ships(id) ON DELETE CASCADE,
  year_built INTEGER,
  max_speed_knots REAL,
  gross_tonnage INTEGER,
  deadweight INTEGER,
  length_meters NUMERIC(10,2),
  beam_meters NUMERIC(10,2),
  draft_meters NUMERIC(10,2)
);

-- ==========================
-- Table: ShipLocationLogs
-- ==========================
CREATE TABLE ShipLocationLogs (
  id SERIAL PRIMARY KEY,
  ship_id INTEGER NOT NULL REFERENCES Ships(id) ON DELETE CASCADE,
  latitude NUMERIC(9,6),
  longitude NUMERIC(9,6),
  speed_knots REAL,
  recorded_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ==========================
-- Table: Voyages
-- ==========================
CREATE TABLE Voyages (
  id SERIAL PRIMARY KEY,
  ship_id INTEGER NOT NULL REFERENCES Ships(id),
  voyage_date DATE NOT NULL,
  departure_port_id INTEGER NOT NULL REFERENCES Ports(id),
  arrival_port_id INTEGER NOT NULL REFERENCES Ports(id),
  start_time TIMESTAMP,
  end_time TIMESTAMP,
  CHECK (departure_port_id <> arrival_port_id)
);

-- ==========================
-- Table: VisitedCountries
-- ==========================
CREATE TABLE VisitedCountries (
  id SERIAL PRIMARY KEY,
  ship_id INTEGER NOT NULL REFERENCES Ships(id),
  country_id INTEGER NOT NULL REFERENCES Countries(id),
  visited_on DATE NOT NULL
);
