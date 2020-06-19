import { IN10Letter, IN3xLetter } from "..";
import { ICodalIndexDataProvider } from "./index";
export declare class CodalIndexDataProvider implements ICodalIndexDataProvider {
    fetchN10Letters(symbol: string): Promise<IN10Letter[]>;
    fetchN3xLetters(symbol: string): Promise<IN3xLetter[]>;
    private static fetchLetters;
    private static rawLetterToN10Letter;
    private static rawLetterToN3xLetter;
    private static parseCodalDateTimeToMillis;
}
//# sourceMappingURL=indexDataProvider.d.ts.map