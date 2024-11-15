create database mcf_test
go

use mcf_test
go

grant connect on database :: mcf_test to dbo
go

create table dbo.ms_storage_location
(
    location_id   varchar(10)  not null collate SQL_Latin1_General_CP1_CI_AS
        constraint ms_storage_location_pk
            primary key,
    location_name varchar(100) not null collate SQL_Latin1_General_CP1_CI_AS
)
go

create table dbo.ms_user
(
    user_id   int identity
        constraint ms_user_pk
            primary key,
    user_name varchar(20) not null collate SQL_Latin1_General_CP1_CI_AS,
    password  varchar(50) not null collate SQL_Latin1_General_CP1_CI_AS,
    is_active bit         not null
)
go

create table dbo.tr_bpkb
(
    agreement_number varchar(100) not null collate SQL_Latin1_General_CP1_CI_AS
        constraint tr_bpkb_pk
            primary key,
    bpkb_no          varchar(100) not null collate SQL_Latin1_General_CP1_CI_AS,
    branch_id        varchar(10) collate SQL_Latin1_General_CP1_CI_AS,
    bpkb_date        datetime     not null,
    faktur_no        varchar(100) not null collate SQL_Latin1_General_CP1_CI_AS,
    faktur_date      datetime     not null,
    location_id      varchar(10)  not null collate SQL_Latin1_General_CP1_CI_AS
        constraint tr_bpkb_ms_storage_location_location_id_fk
            references dbo.ms_storage_location,
    police_no        varchar(20)  not null collate SQL_Latin1_General_CP1_CI_AS,
    bpkb_date_in     datetime     not null,
    created_by       varchar(20)  not null collate SQL_Latin1_General_CP1_CI_AS,
    created_on       datetime     not null,
    last_updated_by  varchar(20)  not null collate SQL_Latin1_General_CP1_CI_AS,
    last_updated_on  datetime     not null
)
go

