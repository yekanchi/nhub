import { Guid } from "guid-typescript";

export class ProcessDto {
    processId: Guid;
    version: Number;
    name: String;
    description: String;
    processParameters: Array<Number>;
}


export class GetProcessParameterDto{
    name: String;
    titile: String;
    type: String;
    isRequired: Boolean;
}