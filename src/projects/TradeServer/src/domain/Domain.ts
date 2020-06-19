import {Entity, PrimaryGeneratedColumn, Column} from "typeorm";
import {ILetter} from "../data/financialStatements";


export class CodalPage {
    Total: number;
    Page: number;
    Letters: Array<Letter>;
}


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
    password: string;

    //this is for es6 initialization
    public constructor(intial?:Partial<User>) {
        Object.assign(this, intial);
    }
}


@Entity({name: "main.letters"})
export class Letter implements ILetter {
    @PrimaryGeneratedColumn({name: "user_id", type: "integer"})
    id: number;

    @Column({type: "text"})
    attachmentUrl: string;

    @Column({type: "integer"})
    code: number;

    @Column({type: "text"})
    companyName: string;

    @Column({type: "integer"})
    publishTime: number;

    @Column({type: "integer"})
    sendTime: number;

    @Column({type: "string"})
    symbol: string;

    @Column({type: "integer"})
    tracingNumber: number;

    @Column({type: "text"})
    url: string;
}




