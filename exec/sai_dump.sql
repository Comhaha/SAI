--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4 (Debian 17.4-1.pgdg120+2)
-- Dumped by pg_dump version 17.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: admin; Type: TABLE; Schema: public; Owner: root
--

CREATE TABLE public.admin (
    admin_id character varying(255) NOT NULL,
    created_at timestamp(6) without time zone NOT NULL,
    status character varying(255) NOT NULL,
    updated_at timestamp(6) without time zone NOT NULL,
    password character varying(255),
    CONSTRAINT admin_status_check CHECK (((status)::text = ANY ((ARRAY['ACTIVE'::character varying, 'INACTIVE'::character varying])::text[])))
);


ALTER TABLE public.admin OWNER TO root;

--
-- Name: token; Type: TABLE; Schema: public; Owner: root
--

CREATE TABLE public.token (
    token_id bigint NOT NULL,
    created_at timestamp(6) without time zone NOT NULL,
    status character varying(255) NOT NULL,
    updated_at timestamp(6) without time zone NOT NULL,
    token character varying(255),
    use_count bigint,
    CONSTRAINT token_status_check CHECK (((status)::text = ANY ((ARRAY['ACTIVE'::character varying, 'INACTIVE'::character varying])::text[])))
);


ALTER TABLE public.token OWNER TO root;

--
-- Name: token_seq; Type: SEQUENCE; Schema: public; Owner: root
--

CREATE SEQUENCE public.token_seq
    START WITH 1
    INCREMENT BY 50
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.token_seq OWNER TO root;

--
-- Data for Name: admin; Type: TABLE DATA; Schema: public; Owner: root
--

COPY public.admin (admin_id, created_at, status, updated_at, password) FROM stdin;
admin	2025-05-13 13:42:38.908235	ACTIVE	2025-05-13 13:42:38.908235	$2a$10$89xq7ckheYdXfz1DFfo7O.CmzYJt5Irw8rDU/f/7VQB57/Q/FL3gm
\.

--
-- Name: token_seq; Type: SEQUENCE SET; Schema: public; Owner: root
--

SELECT pg_catalog.setval('public.token_seq', 851, true);


--
-- Name: admin admin_pkey; Type: CONSTRAINT; Schema: public; Owner: root
--

ALTER TABLE ONLY public.admin
    ADD CONSTRAINT admin_pkey PRIMARY KEY (admin_id);


--
-- Name: token token_pkey; Type: CONSTRAINT; Schema: public; Owner: root
--

ALTER TABLE ONLY public.token
    ADD CONSTRAINT token_pkey PRIMARY KEY (token_id);


--
-- PostgreSQL database dump complete
--

