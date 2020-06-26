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
    public constructor(intial?: Partial<User>) {
        Object.assign(this, intial);
    }
}

@Entity({name: "main.Letters"})
export class Letter /*implements ILetter*/ {
    @PrimaryGeneratedColumn({name: "rowid", type: "integer"})
    id: number;
    @Column({type: "int"})
    tracingNo: number;
    @Column({type: "text"})
    symbol: string;
    @Column({type: "text"})
    companyName: string;
    @Column({type: "integer"})
    underSupervision: boolean;
    @Column({type: "text"})
    title: string;
    @Column({type: "text"})
    letterCode: string;
    @Column({type: "text"})
    sentDateTime: string;
    @Column({type: "text"})
    publishDateTime: string;
    @Column({type: "integer"})
    hasHtml: boolean;
    @Column({type: "text"})
    url: string;
    @Column({type: "integer"})
    hasExcel: boolean;
    @Column({type: "integer"})
    hasPdf: boolean;
    @Column({type: "integer"})
    hasXbrl: boolean;
    @Column({type: "integer"})
    hasAttachment: boolean;
    @Column({type: "text"})
    attachmentUrl: string;
    @Column({type: "text"})
    pdfUrl: string;
    @Column({type: "text"})
    excelUrl: string;
    @Column({type: "text"})
    xbrlUrl: string;
    @Column({type: "text"})
    tedanUrl: string;

    sheets: Array<Sheet> = new Array<Sheet>();

    //m.talebi: this is for es6 initialization
    public constructor(intial?: Partial<Letter>) {
        Object.assign(this, intial);
    }
}

@Entity({name: "main.Sheets"})
export class Sheet /*implements ILetter*/ {
    //شناسه دیتابیس
    @PrimaryGeneratedColumn({name: "rowid", type: "integer"})
    id: number;
    //
    @Column({type: "integer"})
    letterId: number;

    @Column({type: "integer"})
    sheetId: number;

    @Column({type: "text"})
    title: string;

    @Column({type: "text"})
    html: string;

    //m.talebi: this is for es6 initialization
    public constructor(intial?: Partial<Sheet>) {
        Object.assign(this, intial);
    }
}