import {Entity, PrimaryGeneratedColumn, Column} from "typeorm";

@Entity({name: "main.users"})
export class User {
    @PrimaryGeneratedColumn({name: "user_id", type: "integer"})
    id: number;

    @Column({name: "user_name", type: "text"})
    userName: string;

    @Column({name: "name", type: "text"})
    name: string;

    @Column({name: "family", type: "text"})
    family: string;

    @Column({name: "email", type: "text"})
    email: string;

    @Column({name: "password_hash", type: "text"})
    passsowrd: string;


    public constructor(intial?:Partial<User>) {
        Object.assign(this, intial);
    }
}




