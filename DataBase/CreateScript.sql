-- Table: public.Utilizador

-- DROP TABLE IF EXISTS public."Utilizador";

CREATE TABLE IF NOT EXISTS public."Utilizador"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Username" text COLLATE pg_catalog."default" NOT NULL,
    "Password" text COLLATE pg_catalog."default" NOT NULL,
    "Admin" boolean NOT NULL,
    "Estado" boolean NOT NULL,
    CONSTRAINT "Utilizador_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Utilizador"
    OWNER to juritech;

-- Table: public.Job

-- DROP TABLE IF EXISTS public."Job";

CREATE TABLE IF NOT EXISTS public."Job"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Nome" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Job_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Job"
    OWNER to juritech;

-- Table: public.Maquina

-- DROP TABLE IF EXISTS public."Maquina";

CREATE TABLE IF NOT EXISTS public."Maquina"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Nome" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Maquina_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Maquina"
    OWNER to juritech;

-- Table: public.Operacao

-- DROP TABLE IF EXISTS public."Operacao";

CREATE TABLE IF NOT EXISTS public."Operacao"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Nome" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Operacao_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Operacao"
    OWNER to juritech;

-- Table: public.Simulacao

-- DROP TABLE IF EXISTS public."Simulacao";

CREATE TABLE IF NOT EXISTS public."Simulacao"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Nome" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Simulacao_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Simulacao"
    OWNER to juritech;

-- Table: public.Plano

-- DROP TABLE IF EXISTS public."Plano";

CREATE TABLE IF NOT EXISTS public."Plano"
(
    "IdUtilizador" integer NOT NULL,
    "IdSimulacao" integer NOT NULL,
    "IdJob" integer NOT NULL,
    "IdOperacao" integer NOT NULL,
    "IdMaquina" integer NOT NULL,
    "TempoInicial" integer,
    "UnidadeTempo" integer NOT NULL,
    "TempoFinal" integer,
    "Estado" boolean NOT NULL,
    "PosOperacao" integer NOT NULL,
    CONSTRAINT "Plano_pkey" PRIMARY KEY ("IdJob", "IdOperacao", "IdSimulacao", "IdUtilizador"),
    CONSTRAINT "JobFK" FOREIGN KEY ("IdJob")
        REFERENCES public."Job" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "MaquinaFK" FOREIGN KEY ("IdMaquina")
        REFERENCES public."Maquina" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "OperacaoFK" FOREIGN KEY ("IdOperacao")
        REFERENCES public."Operacao" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "SimulacaoFK" FOREIGN KEY ("IdSimulacao")
        REFERENCES public."Simulacao" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Plano"
    OWNER to juritech;