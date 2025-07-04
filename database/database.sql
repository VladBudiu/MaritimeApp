PGDMP  /    "                }           MaritimeApp    17.4    17.4 H               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false                       1262    32863    MaritimeApp    DATABASE     s   CREATE DATABASE "MaritimeApp" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en-US';
    DROP DATABASE "MaritimeApp";
                     postgres    false            �            1259    32924 	   companies    TABLE     `   CREATE TABLE public.companies (
    id integer NOT NULL,
    name character varying NOT NULL
);
    DROP TABLE public.companies;
       public         heap r       postgres    false            �            1259    32923    companies_id_seq    SEQUENCE     �   CREATE SEQUENCE public.companies_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.companies_id_seq;
       public               postgres    false    226                       0    0    companies_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.companies_id_seq OWNED BY public.companies.id;
          public               postgres    false    225            �            1259    32865 	   countries    TABLE     `   CREATE TABLE public.countries (
    id integer NOT NULL,
    name character varying NOT NULL
);
    DROP TABLE public.countries;
       public         heap r       postgres    false            �            1259    32864    countries_id_seq    SEQUENCE     �   CREATE SEQUENCE public.countries_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.countries_id_seq;
       public               postgres    false    218                       0    0    countries_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.countries_id_seq OWNED BY public.countries.id;
          public               postgres    false    217            �            1259    32876    ports    TABLE     }   CREATE TABLE public.ports (
    id integer NOT NULL,
    name character varying NOT NULL,
    country_id integer NOT NULL
);
    DROP TABLE public.ports;
       public         heap r       postgres    false            �            1259    32875    ports_id_seq    SEQUENCE     �   CREATE SEQUENCE public.ports_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.ports_id_seq;
       public               postgres    false    220                        0    0    ports_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.ports_id_seq OWNED BY public.ports.id;
          public               postgres    false    219            �            1259    32935    shipcompanyrelations    TABLE     D  CREATE TABLE public.shipcompanyrelations (
    id integer NOT NULL,
    ship_id integer NOT NULL,
    company_id integer NOT NULL,
    role character varying NOT NULL,
    CONSTRAINT shipcompanyrelations_role_check CHECK (((role)::text = ANY ((ARRAY['Owner'::character varying, 'Operator'::character varying])::text[])))
);
 (   DROP TABLE public.shipcompanyrelations;
       public         heap r       postgres    false            �            1259    32934    shipcompanyrelations_id_seq    SEQUENCE     �   CREATE SEQUENCE public.shipcompanyrelations_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE public.shipcompanyrelations_id_seq;
       public               postgres    false    228            !           0    0    shipcompanyrelations_id_seq    SEQUENCE OWNED BY     [   ALTER SEQUENCE public.shipcompanyrelations_id_seq OWNED BY public.shipcompanyrelations.id;
          public               postgres    false    227            �            1259    32969    shiplocationlogs    TABLE     �   CREATE TABLE public.shiplocationlogs (
    id integer NOT NULL,
    ship_id integer NOT NULL,
    latitude numeric(9,6),
    longitude numeric(9,6),
    speed_knots real,
    recorded_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);
 $   DROP TABLE public.shiplocationlogs;
       public         heap r       postgres    false            �            1259    32968    shiplocationlogs_id_seq    SEQUENCE     �   CREATE SEQUENCE public.shiplocationlogs_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.shiplocationlogs_id_seq;
       public               postgres    false    232            "           0    0    shiplocationlogs_id_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.shiplocationlogs_id_seq OWNED BY public.shiplocationlogs.id;
          public               postgres    false    231            �            1259    32903    ships    TABLE     �   CREATE TABLE public.ships (
    id integer NOT NULL,
    name character varying NOT NULL,
    imo_number character varying(10) NOT NULL,
    ship_type_id integer,
    flag_country_id integer
);
    DROP TABLE public.ships;
       public         heap r       postgres    false            �            1259    32902    ships_id_seq    SEQUENCE     �   CREATE SEQUENCE public.ships_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.ships_id_seq;
       public               postgres    false    224            #           0    0    ships_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.ships_id_seq OWNED BY public.ships.id;
          public               postgres    false    223            �            1259    32957    shipspecifications    TABLE     '  CREATE TABLE public.shipspecifications (
    id integer NOT NULL,
    ship_id integer NOT NULL,
    year_built integer,
    max_speed_knots real,
    gross_tonnage integer,
    deadweight integer,
    length_meters numeric(10,2),
    beam_meters numeric(10,2),
    draft_meters numeric(10,2)
);
 &   DROP TABLE public.shipspecifications;
       public         heap r       postgres    false            �            1259    32956    shipspecifications_id_seq    SEQUENCE     �   CREATE SEQUENCE public.shipspecifications_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.shipspecifications_id_seq;
       public               postgres    false    230            $           0    0    shipspecifications_id_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.shipspecifications_id_seq OWNED BY public.shipspecifications.id;
          public               postgres    false    229            �            1259    32892 	   shiptypes    TABLE     e   CREATE TABLE public.shiptypes (
    id integer NOT NULL,
    type_name character varying NOT NULL
);
    DROP TABLE public.shiptypes;
       public         heap r       postgres    false            �            1259    32891    shiptypes_id_seq    SEQUENCE     �   CREATE SEQUENCE public.shiptypes_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.shiptypes_id_seq;
       public               postgres    false    222            %           0    0    shiptypes_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.shiptypes_id_seq OWNED BY public.shiptypes.id;
          public               postgres    false    221            �            1259    33005    visitedcountries    TABLE     �   CREATE TABLE public.visitedcountries (
    id integer NOT NULL,
    ship_id integer NOT NULL,
    country_id integer NOT NULL,
    visited_on date NOT NULL
);
 $   DROP TABLE public.visitedcountries;
       public         heap r       postgres    false            �            1259    33004    visitedcountries_id_seq    SEQUENCE     �   CREATE SEQUENCE public.visitedcountries_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.visitedcountries_id_seq;
       public               postgres    false    236            &           0    0    visitedcountries_id_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.visitedcountries_id_seq OWNED BY public.visitedcountries.id;
          public               postgres    false    235            �            1259    32982    voyages    TABLE     g  CREATE TABLE public.voyages (
    id integer NOT NULL,
    ship_id integer NOT NULL,
    voyage_date date NOT NULL,
    departure_port_id integer NOT NULL,
    arrival_port_id integer NOT NULL,
    start_time timestamp without time zone,
    end_time timestamp without time zone,
    CONSTRAINT voyages_check CHECK ((departure_port_id <> arrival_port_id))
);
    DROP TABLE public.voyages;
       public         heap r       postgres    false            �            1259    32981    voyages_id_seq    SEQUENCE     �   CREATE SEQUENCE public.voyages_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.voyages_id_seq;
       public               postgres    false    234            '           0    0    voyages_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.voyages_id_seq OWNED BY public.voyages.id;
          public               postgres    false    233            R           2604    32927    companies id    DEFAULT     l   ALTER TABLE ONLY public.companies ALTER COLUMN id SET DEFAULT nextval('public.companies_id_seq'::regclass);
 ;   ALTER TABLE public.companies ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    225    226    226            N           2604    32868    countries id    DEFAULT     l   ALTER TABLE ONLY public.countries ALTER COLUMN id SET DEFAULT nextval('public.countries_id_seq'::regclass);
 ;   ALTER TABLE public.countries ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    217    218    218            O           2604    32879    ports id    DEFAULT     d   ALTER TABLE ONLY public.ports ALTER COLUMN id SET DEFAULT nextval('public.ports_id_seq'::regclass);
 7   ALTER TABLE public.ports ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    219    220    220            S           2604    32938    shipcompanyrelations id    DEFAULT     �   ALTER TABLE ONLY public.shipcompanyrelations ALTER COLUMN id SET DEFAULT nextval('public.shipcompanyrelations_id_seq'::regclass);
 F   ALTER TABLE public.shipcompanyrelations ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    228    227    228            U           2604    32972    shiplocationlogs id    DEFAULT     z   ALTER TABLE ONLY public.shiplocationlogs ALTER COLUMN id SET DEFAULT nextval('public.shiplocationlogs_id_seq'::regclass);
 B   ALTER TABLE public.shiplocationlogs ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    231    232    232            Q           2604    32906    ships id    DEFAULT     d   ALTER TABLE ONLY public.ships ALTER COLUMN id SET DEFAULT nextval('public.ships_id_seq'::regclass);
 7   ALTER TABLE public.ships ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    223    224    224            T           2604    32960    shipspecifications id    DEFAULT     ~   ALTER TABLE ONLY public.shipspecifications ALTER COLUMN id SET DEFAULT nextval('public.shipspecifications_id_seq'::regclass);
 D   ALTER TABLE public.shipspecifications ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    230    229    230            P           2604    32895    shiptypes id    DEFAULT     l   ALTER TABLE ONLY public.shiptypes ALTER COLUMN id SET DEFAULT nextval('public.shiptypes_id_seq'::regclass);
 ;   ALTER TABLE public.shiptypes ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    222    221    222            X           2604    33008    visitedcountries id    DEFAULT     z   ALTER TABLE ONLY public.visitedcountries ALTER COLUMN id SET DEFAULT nextval('public.visitedcountries_id_seq'::regclass);
 B   ALTER TABLE public.visitedcountries ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    236    235    236            W           2604    32985 
   voyages id    DEFAULT     h   ALTER TABLE ONLY public.voyages ALTER COLUMN id SET DEFAULT nextval('public.voyages_id_seq'::regclass);
 9   ALTER TABLE public.voyages ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    233    234    234            l           2606    32933    companies companies_name_key 
   CONSTRAINT     W   ALTER TABLE ONLY public.companies
    ADD CONSTRAINT companies_name_key UNIQUE (name);
 F   ALTER TABLE ONLY public.companies DROP CONSTRAINT companies_name_key;
       public                 postgres    false    226            n           2606    32931    companies companies_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.companies
    ADD CONSTRAINT companies_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.companies DROP CONSTRAINT companies_pkey;
       public                 postgres    false    226            \           2606    32874    countries countries_name_key 
   CONSTRAINT     W   ALTER TABLE ONLY public.countries
    ADD CONSTRAINT countries_name_key UNIQUE (name);
 F   ALTER TABLE ONLY public.countries DROP CONSTRAINT countries_name_key;
       public                 postgres    false    218            ^           2606    32872    countries countries_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.countries
    ADD CONSTRAINT countries_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.countries DROP CONSTRAINT countries_pkey;
       public                 postgres    false    218            `           2606    32885    ports ports_name_country_id_key 
   CONSTRAINT     f   ALTER TABLE ONLY public.ports
    ADD CONSTRAINT ports_name_country_id_key UNIQUE (name, country_id);
 I   ALTER TABLE ONLY public.ports DROP CONSTRAINT ports_name_country_id_key;
       public                 postgres    false    220    220            b           2606    32883    ports ports_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.ports
    ADD CONSTRAINT ports_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.ports DROP CONSTRAINT ports_pkey;
       public                 postgres    false    220            p           2606    32943 .   shipcompanyrelations shipcompanyrelations_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public.shipcompanyrelations
    ADD CONSTRAINT shipcompanyrelations_pkey PRIMARY KEY (id);
 X   ALTER TABLE ONLY public.shipcompanyrelations DROP CONSTRAINT shipcompanyrelations_pkey;
       public                 postgres    false    228            r           2606    32945 E   shipcompanyrelations shipcompanyrelations_ship_id_company_id_role_key 
   CONSTRAINT     �   ALTER TABLE ONLY public.shipcompanyrelations
    ADD CONSTRAINT shipcompanyrelations_ship_id_company_id_role_key UNIQUE (ship_id, company_id, role);
 o   ALTER TABLE ONLY public.shipcompanyrelations DROP CONSTRAINT shipcompanyrelations_ship_id_company_id_role_key;
       public                 postgres    false    228    228    228            v           2606    32975 &   shiplocationlogs shiplocationlogs_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.shiplocationlogs
    ADD CONSTRAINT shiplocationlogs_pkey PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.shiplocationlogs DROP CONSTRAINT shiplocationlogs_pkey;
       public                 postgres    false    232            h           2606    32912    ships ships_imo_number_key 
   CONSTRAINT     [   ALTER TABLE ONLY public.ships
    ADD CONSTRAINT ships_imo_number_key UNIQUE (imo_number);
 D   ALTER TABLE ONLY public.ships DROP CONSTRAINT ships_imo_number_key;
       public                 postgres    false    224            j           2606    32910    ships ships_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.ships
    ADD CONSTRAINT ships_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.ships DROP CONSTRAINT ships_pkey;
       public                 postgres    false    224            t           2606    32962 *   shipspecifications shipspecifications_pkey 
   CONSTRAINT     h   ALTER TABLE ONLY public.shipspecifications
    ADD CONSTRAINT shipspecifications_pkey PRIMARY KEY (id);
 T   ALTER TABLE ONLY public.shipspecifications DROP CONSTRAINT shipspecifications_pkey;
       public                 postgres    false    230            d           2606    32899    shiptypes shiptypes_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.shiptypes
    ADD CONSTRAINT shiptypes_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.shiptypes DROP CONSTRAINT shiptypes_pkey;
       public                 postgres    false    222            f           2606    32901 !   shiptypes shiptypes_type_name_key 
   CONSTRAINT     a   ALTER TABLE ONLY public.shiptypes
    ADD CONSTRAINT shiptypes_type_name_key UNIQUE (type_name);
 K   ALTER TABLE ONLY public.shiptypes DROP CONSTRAINT shiptypes_type_name_key;
       public                 postgres    false    222            z           2606    33010 &   visitedcountries visitedcountries_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.visitedcountries
    ADD CONSTRAINT visitedcountries_pkey PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.visitedcountries DROP CONSTRAINT visitedcountries_pkey;
       public                 postgres    false    236            x           2606    32988    voyages voyages_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.voyages
    ADD CONSTRAINT voyages_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.voyages DROP CONSTRAINT voyages_pkey;
       public                 postgres    false    234            {           2606    32886    ports ports_country_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.ports
    ADD CONSTRAINT ports_country_id_fkey FOREIGN KEY (country_id) REFERENCES public.countries(id);
 E   ALTER TABLE ONLY public.ports DROP CONSTRAINT ports_country_id_fkey;
       public               postgres    false    218    220    4702            ~           2606    32951 9   shipcompanyrelations shipcompanyrelations_company_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.shipcompanyrelations
    ADD CONSTRAINT shipcompanyrelations_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.companies(id);
 c   ALTER TABLE ONLY public.shipcompanyrelations DROP CONSTRAINT shipcompanyrelations_company_id_fkey;
       public               postgres    false    228    226    4718                       2606    32946 6   shipcompanyrelations shipcompanyrelations_ship_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.shipcompanyrelations
    ADD CONSTRAINT shipcompanyrelations_ship_id_fkey FOREIGN KEY (ship_id) REFERENCES public.ships(id) ON DELETE CASCADE;
 `   ALTER TABLE ONLY public.shipcompanyrelations DROP CONSTRAINT shipcompanyrelations_ship_id_fkey;
       public               postgres    false    224    228    4714            �           2606    32976 .   shiplocationlogs shiplocationlogs_ship_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.shiplocationlogs
    ADD CONSTRAINT shiplocationlogs_ship_id_fkey FOREIGN KEY (ship_id) REFERENCES public.ships(id) ON DELETE CASCADE;
 X   ALTER TABLE ONLY public.shiplocationlogs DROP CONSTRAINT shiplocationlogs_ship_id_fkey;
       public               postgres    false    4714    232    224            |           2606    32918     ships ships_flag_country_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.ships
    ADD CONSTRAINT ships_flag_country_id_fkey FOREIGN KEY (flag_country_id) REFERENCES public.countries(id);
 J   ALTER TABLE ONLY public.ships DROP CONSTRAINT ships_flag_country_id_fkey;
       public               postgres    false    218    224    4702            }           2606    32913    ships ships_ship_type_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.ships
    ADD CONSTRAINT ships_ship_type_id_fkey FOREIGN KEY (ship_type_id) REFERENCES public.shiptypes(id);
 G   ALTER TABLE ONLY public.ships DROP CONSTRAINT ships_ship_type_id_fkey;
       public               postgres    false    222    4708    224            �           2606    32963 2   shipspecifications shipspecifications_ship_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.shipspecifications
    ADD CONSTRAINT shipspecifications_ship_id_fkey FOREIGN KEY (ship_id) REFERENCES public.ships(id) ON DELETE CASCADE;
 \   ALTER TABLE ONLY public.shipspecifications DROP CONSTRAINT shipspecifications_ship_id_fkey;
       public               postgres    false    4714    230    224            �           2606    33016 1   visitedcountries visitedcountries_country_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.visitedcountries
    ADD CONSTRAINT visitedcountries_country_id_fkey FOREIGN KEY (country_id) REFERENCES public.countries(id);
 [   ALTER TABLE ONLY public.visitedcountries DROP CONSTRAINT visitedcountries_country_id_fkey;
       public               postgres    false    236    4702    218            �           2606    33011 .   visitedcountries visitedcountries_ship_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.visitedcountries
    ADD CONSTRAINT visitedcountries_ship_id_fkey FOREIGN KEY (ship_id) REFERENCES public.ships(id);
 X   ALTER TABLE ONLY public.visitedcountries DROP CONSTRAINT visitedcountries_ship_id_fkey;
       public               postgres    false    224    236    4714            �           2606    32999 $   voyages voyages_arrival_port_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.voyages
    ADD CONSTRAINT voyages_arrival_port_id_fkey FOREIGN KEY (arrival_port_id) REFERENCES public.ports(id);
 N   ALTER TABLE ONLY public.voyages DROP CONSTRAINT voyages_arrival_port_id_fkey;
       public               postgres    false    220    234    4706            �           2606    32994 &   voyages voyages_departure_port_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.voyages
    ADD CONSTRAINT voyages_departure_port_id_fkey FOREIGN KEY (departure_port_id) REFERENCES public.ports(id);
 P   ALTER TABLE ONLY public.voyages DROP CONSTRAINT voyages_departure_port_id_fkey;
       public               postgres    false    4706    234    220            �           2606    32989    voyages voyages_ship_id_fkey    FK CONSTRAINT     {   ALTER TABLE ONLY public.voyages
    ADD CONSTRAINT voyages_ship_id_fkey FOREIGN KEY (ship_id) REFERENCES public.ships(id);
 F   ALTER TABLE ONLY public.voyages DROP CONSTRAINT voyages_ship_id_fkey;
       public               postgres    false    234    4714    224           