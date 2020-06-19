export interface IPeriodic {
    period: number;
    rpYear: number;
    rpMonth: number;
}
export interface IAuditable {
    audited: boolean;
}
export interface IConsolidatable {
    consolidated: boolean;
}
export interface ILetter {
    tracingNumber: number;
    symbol: string;
    companyName: string;
    code: number;
    sendTime: number;
    publishTime: number;
    url: string;
    attachmentUrl: string;
}
export interface IReport extends ILetter {
    smYear: number;
    smMonth: number;
    capital: number;
    amend: boolean;
    amendmentDesc: string;
    amendedTracingNumber: number;
}
export interface IPeriodicLetter extends ILetter, IPeriodic {
}
export interface IPeriodicReport extends IReport, IPeriodic {
}
export interface IN3xLetter extends IPeriodicLetter {
    period: 1;
}
export declare enum N30Type {
    UNKNOWN = 0,
    PRODUCT = 1,
    CONTRACT = 2,
    BANK = 3,
    LEASING = 4
}
export interface IN30Report extends IPeriodicReport {
    monthIncome: number;
    yearIncome: number;
    monthDesc: string;
    yearDesc: string;
    type: N30Type;
    period: 1;
}
export declare enum N30MarketType {
    UNKNOWN = 0,
    INTERNAL = 1,
    EXTERNAL = 2
}
export interface IN30ProductItem {
    name: string;
    unit: string;
    mQuantityProd: number;
    mQuantitySell: number;
    mFee: number;
    mValue: number;
    tQuantityProd: number;
    tQuantitySell: number;
    tFee: number;
    tValue: number;
    marketType: N30MarketType;
}
export interface IN30ContractItem {
    name: string;
    conDate: string;
    conDuration: number;
    mValue: number;
    tValue: number;
}
export declare type IN30Item = IN30ProductItem | IN30ContractItem;
export interface IN31Report extends IPeriodicReport {
    period: 1;
}
export interface IN31IndustryGroupInvestmentItem {
    groupName: string;
    inStartCompanyCount: number;
    inStartCost: number;
    inStartMarketValue: number;
    inDiffCost: number;
    inDiffMarketValue: number;
    inEndCompanyCount: number;
    inEndCost: number;
    inEndMarketValue: number;
    outStartCompanyCount: number;
    outStartCost: number;
    outDiffCost: number;
    outEndCompanyCount: number;
    outEndCost: number;
    totStartCompanyCount: number;
    totStartCost: number;
    totStartPercent: number;
    totDiffCost: number;
    totDiffPercent: number;
    totEndCompanyCount: number;
    totEndCost: number;
    totEndPercent: number;
}
export interface IN31PeriodicPortfolioInItem {
    companyName: string;
    capital: number;
    parValue: number;
    startSharesCount: number;
    startCost: number;
    startMarketValue: number;
    diffSharesCount: number;
    diffCost: number;
    diffMarketValue: number;
    endPercent: number;
    endCost: number;
    endMarketValue: number;
    endPerShareCost: number;
    endPerShareValue: number;
    endDiff: number;
}
export interface IN31PeriodicPortfolioOutItem {
    companyName: string;
    capital: number;
    parValue: number;
    startSharesCount: number;
    startCost: number;
    diffSharesCount: number;
    diffCost: number;
    endPercent: number;
    endCost: number;
    endPerShareCost: number;
    desc: string;
}
export interface IN31BuyInvestmentItem {
    companyName: string;
    sharesCount: number;
    perShareCost: number;
    totalCostIn: number;
    totalCostOut: number;
}
export interface IN31SellInvestmentItem {
    companyName: string;
    sharesCount: number;
    perShareCost: number;
    totalCost: number;
    perShareSellPrice: number;
    sellValue: number;
    profitOrLoss: number;
}
export interface IN31DividentIncomeItem {
    companyName: string;
    saleMali: string;
    meetingDate: string;
    sharesCount: number;
    capital: number;
    parValue: number;
    percent: number;
    eps: number;
    dps: number;
    income: number;
}
export interface IN10Letter extends IPeriodicLetter, IAuditable, IConsolidatable {
}
export interface IN10Report extends IPeriodicReport, IAuditable, IConsolidatable {
}
export interface IIncomeStatement extends IPeriodic, IAuditable {
    dividendIncome?: number;
    interestIncome?: number;
    salesOfInvestmentInSecuritiesPL?: number;
    changesInFairValueOfInvestmentInSecuritiesPL?: number;
    revenue?: number;
    costOfSales?: number;
    grossProfit?: number;
    sgaExpenses: number;
    hazineKaheshArzeshDaryaftaniha?: number;
    otherRevenues?: number;
    otherExpenses?: number;
    otherRevenuesAndExpenses?: number;
    operatingProfit: number;
    financeCosts: number;
    nonOperatingPL_investments?: number;
    nonOperatingPL_others?: number;
    nonOperatingPL?: number;
    continuingOperationsProfitBeforeTax: number;
    incomeTaxCurrentYear?: number;
    incomeTaxPastYears?: number;
    incomeTaxExpense?: number;
    continuingOperationsProfit?: number;
    discontinuedOperationsProfit?: number;
    profit: number;
    operatingBasicEPS?: number;
    nonOperatingBasicEPS?: number;
    continuingOperationsBasicEPS?: number;
    discontinuedOperationsBasicEPS?: number;
    basicEPS?: number;
    eps: number;
    capital: number;
}
export interface IBalanceSheet extends IPeriodic, IAuditable {
    cashAndCachEquivalents: number;
    shortTermInvestments: number;
    tradeAndNonTradeReceivables?: number;
    tradeReceivables?: number;
    nonTradeReceivables?: number;
    receivablesFromAffiliatedCompanies?: number;
    inventories: number;
    prepayments: number;
    advancesToSupliers?: number;
    nonCurrentAssetsForSale?: number;
    totalCurrentAssets: number;
    longTermNotesAndAccountsReceivable: number;
    longTermInvestments: number;
    investmentProperty?: number;
    intangibleAssets: number;
    plantAssetsNetOfAccumulatedDepreciation: number;
    capitalAdvances?: number;
    otherNonCurrentAssets: number;
    totalNonCurrentAssets: number;
    totalAssets: number;
    tradeAndNonTradePayables?: number;
    tradePayables?: number;
    nonTradePayables?: number;
    debtToAffiliatedComponies?: number;
    deferredRevenue: number;
    currentTaxLiabilities: number;
    dividentsPayable: number;
    loanPayableCurrent: number;
    provisions?: number;
    liabilitiesOfDisposalGroupsForSale?: number;
    totalCurrentLiabilities: number;
    longTermPayables: number;
    nonCurrentDeferredRevenue?: number;
    loanPayable: number;
    allowanceForPostRetirement: number;
    totalNonCurrentLiabilities: number;
    totalLiabilities: number;
    commonStock: number;
    inprocessCapitalIncrease: number;
    sharePremium: number;
    treasuryStock?: number;
    treasuryStockConsume?: number;
    receivesForCapitalAdvance?: number;
    legalReserve: number;
    expansionReserve: number;
    retainedEarnings: number;
    revaluationSurplus?: number;
    revaluationSurplusOfNonCurrentAssetsForSale?: number;
    exchangeDifferences?: number;
    exchangeReserveOfGovernmentalCorporations?: number;
    totalStockHoldersEquity: number;
    totalLiabilitiesAndStockHoldersEquity: number;
}
export interface ISaleAndCost extends IPeriodic {
    sale: number;
    cost: number;
}
export interface ICostOfGoods extends IPeriodic {
    mavadMostaghimMasrafi: number;
    dastmozdMostaghimTolid: number;
    sarbarTolid: number;
    jame: number;
    hazinehJazbNashodehDarTolid: number;
    jameHazinehayeTolid: number;
    mojodiKalaDarJaryanSakhtAvalDoreh: number;
    mojodiKalaDarJaryanSakhtPayanDoreh: number;
    zayeatGhireAdi: number;
    bahayeTamamShodeKalaTolidShode: number;
    mojodiKalaSakhtehShodeAvalDoreh: number;
    mojodiKalaSakhtehShodePayanDoreh: number;
    bahayeTamamShodeKalaForoshRafteh: number;
    khadamat: number;
    jameBahayeTamamShode: number;
}
//# sourceMappingURL=index.d.ts.map