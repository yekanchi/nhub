
create table users
(
	id integer not null
		constraint users_pk
			primary key autoincrement,
	uid text not null,
	name text null ,
	family text null,
	email text null
);

create unique index users_id_uindex
	on users (user_id);

create unique index users_uid_uindex
	on users (user_uid);

