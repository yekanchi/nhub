export interface ICodalIndexDataProvider {
    fetchN10Letters(symbol: string): Promise<IN10Letter[]>,

    fetchN3xLetters(symbol: string): Promise<IN3xLetter[]>,
}

export interface ICodalN30ReportDataProvider {
    parseReport(): Promise<IN30Report>,

    parseItems(): Promise<IN30Item[]>,
}

export interface ICodalN10ReportDataProvider {
    parseReport(): Promise<IN10Report>,

    parseIncomeStatements(): Promise<IIncomeStatement[]>,

    parseBalanceSheets(): Promise<IBalanceSheet[]>,

    parseSalesAndCostsTrend(): Promise<ISaleAndCost[]>,

    parseCostOfGoods(): Promise<ICostOfGoods[]>,

    parseFutureManagementGoalsAndStrategies(): Promise<string>,
}

export interface IPeriodic {
    // طول دوره
    period: number,
    // سال انتهای دوره
    rpYear: number,
    // ماه انتهای دوره
    rpMonth: number,
}

export interface IAuditable {
    // حسابرسی شده
    audited: boolean,
}

export interface IConsolidatable {
    // تلفیقی
    consolidated: boolean,
}

export interface ILetter {
    // شماره پیگیری
    tracingNumber: number,
    // نماد
    symbol: string,
    // نام شرکت
    companyName: string,
    // کد نوع گزارش
    code: number,
    // زمان ارسال
    sendTime: number,
    // زمان انتشار
    publishTime: number,
    // آدرس اینترنتی گزارش
    url: string,
    // آدرس اینترنتی فایل ضمیمه گزارش
    attachmentUrl: string,
}

export interface IReport extends ILetter {
    // سال مالی (سال)
    smYear: number,
    // سال مالی (ماه)
    smMonth: number,
    // سرمایه
    capital: number,
    // اصلاحیه
    amend: boolean,
    // توضیحات اصلاحیه
    amendmentDesc: string,
    // شماره پیگیری گزارش اصلاح شده
    amendedTracingNumber: number,
}

export interface IPeriodicLetter extends ILetter, IPeriodic {
}

export interface IPeriodicReport extends IReport, IPeriodic {
}

export interface IN10Letter extends IPeriodicLetter, IAuditable, IConsolidatable {
}

export interface IN10Report extends IPeriodicReport, IAuditable, IConsolidatable {
}

export interface IN3xLetter extends IPeriodicLetter {
    period: 1
}

export enum N30Type {
    // نامشخص
    UNKNOWN = 0,
    // تولیدی
    PRODUCT = 1,
    // قراردادی
    CONTRACT = 2,
    // بانک
    BANK = 3,
    // لیزینگ
    LEASING = 4,
}

export interface IN30Report extends IPeriodicReport {
    // جمع مبالغ فروش طی دوره یک ماهه جاری
    monthIncome: number,
    // جمع مبالغ فروش از ابتدای سال مالی تا پایان دوره یک ماهه جاری
    yearIncome: number,
    // توضیحات دوره یک ماهه جاری
    monthDesc: string,
    // توضیحات دوره از ابتدای سال مالی تا پایان دوره یک ماهه جاری
    yearDesc: string,
    // نوع گزارش
    type: N30Type,
    // طول دوره
    period: 1,
}

export enum N30MarketType {
    // نامشخص
    UNKNOWN = 0,
    // فروش داخلی
    INTERNAL = 1,
    // فروش صادراتی
    EXTERNAL = 2,
}

export interface IN30ProductItem {
    // نام محصول
    name: string,
    // واحد
    unit: string,
    // میزان تولید در دوره
    mQuantityProd: number,
    // میزان فروش در دوره
    mQuantitySell: number,
    // نرخ فروش در دوره
    mFee: number,
    // مبلغ فروش در دوره
    mValue: number,
    // میزان تولید از ابتدای سال مالی تا پایان دوره
    tQuantityProd: number,
    // میزان فروش از ابتدای سال مالی تا پایان دوره
    tQuantitySell: number,
    // نرخ فروش از ابتدای سال مالی تا پایان دوره
    tFee: number,
    // مبلغ فروش از ابتدای سال مالی تا پایان دوره
    tValue: number,
    // بازار فروش
    marketType: N30MarketType,
}

export interface IN30ContractItem {
    // نام قرارداد
    name: string,
    // تاریخ عقد قرارداد
    conDate: string,
    // مدت زمان قرارداد به ماه
    conDuration: number,
    // درآمد شناسایی شده طی دوره
    mValue: number,
    // درآمد شناسایی شده از ابتدای سال مالی تا پایان دوره
    tValue: number,
}

export type IN30Item = IN30ProductItem | IN30ContractItem;

export interface IIncomeStatement extends IPeriodic, IAuditable {
    // درآمد سود سهام
    dividendIncome?: number,
    // درآمد سود تضمین شده
    interestIncome?: number,
    // سود (زیان) فروش سرمایه گذاری ها
    salesOfInvestmentInSecuritiesPL?: number,
    // سود (زیان) تغییر ارزش سرمایه گذاری در اوراق بهادار
    changesInFairValueOfInvestmentInSecuritiesPL?: number,
    // درآمدهای عملیاتی
    revenue?: number,
    //بهای تمام شده درآمدهای عملیاتی
    costOfSales?: number,
    // سود (زیان) ناخالص
    grossProfit?: number,
    // هزینه‌های فروش، اداری و عمومی
    sgaExpenses: number,
    // هزینه کاهش ارزش دریافتنی‌ها (هزینه استثنایی)
    hazineKaheshArzeshDaryaftaniha?: number,
    // سایر درآمدها
    otherRevenues?: number,
    // سایر هزینه‌ها
    otherExpenses?: number,
    // سایر درآمدها (هزینه های) عملیاتی
    otherRevenuesAndExpenses?: number,
    // سود (زیان) عملیاتی
    operatingProfit: number,
    // هزینه‌های مالی
    financeCosts: number,
    // سایر درآمدها و هزینه‌های غیر عملیاتی - درآمد سرمایه‌گذاری‌ها
    nonOperatingPL_investments?: number,
    // سایر درآمدها و هزینه‌های غیر عملیاتی - اقلام متفرقه
    nonOperatingPL_others?: number,
    // خالص سایر درآمدها و هزینه های غیرعملیاتی
    nonOperatingPL?: number,
    // سود (زیان) عملیات در حال تداوم قبل از مالیات
    continuingOperationsProfitBeforeTax: number,
    // هزینه مالیات بر درآمد سال جاری
    incomeTaxCurrentYear?: number,
    // هزینه مالیات بر درآمد سال‌های قبل
    incomeTaxPastYears?: number,
    // مالیات بر درآمد
    incomeTaxExpense?: number,
    // سود (زیان) خالص عملیات در حال تداوم
    continuingOperationsProfit?: number,
    // سود (زیان) خالص عملیات متوقف شده
    discontinuedOperationsProfit?: number,
    // سود (زیان) خالص
    profit: number,
    // سود عملیاتی پایه هر سهم
    operatingBasicEPS?: number,
    // سود غیر عملیاتی پابه هر سهم
    nonOperatingBasicEPS?: number,
    // سود پایه هر سهم ناشی از عملیات در حال تداوم
    continuingOperationsBasicEPS?: number,
    // سود پایه هر سهم ناشی از عملیات متوقف شده
    discontinuedOperationsBasicEPS?: number,
    // سود (زیان) پایه هر سهم
    basicEPS?: number,
    // سود (زیان) خالص هر سهم
    eps: number,
    // سرمایه
    capital: number,
}

export interface IBalanceSheet extends IPeriodic, IAuditable {
    // *** دارایی‌ها ***
    // * دارایی‌های جاری *
    // موجودی نقد
    cashAndCachEquivalents: number,
    // سرمایه‌گذاری‌های کوتاه مدت
    shortTermInvestments: number,
    // دریافتنی‌های تجاری و سایر دریافتنی‌ها
    tradeAndNonTradeReceivables?: number,
    // دریافتنی‌‌های تجاری
    tradeReceivables?: number,
    // دریافتنی‌‌های غیرتجاری
    nonTradeReceivables?: number,
    // طلب از شرکتهای گروه و شرکتهای وابسته
    receivablesFromAffiliatedCompanies?: number,
    // موجودی مواد و کالا
    inventories: number,
    // پیش پرداختها
    prepayments: number,
    // سفارشات
    advancesToSupliers?: number,
    // دارایی‌های غیر جاری نگهداری شده برای فروش
    nonCurrentAssetsForSale?: number,
    // جمع دارایی‌های جاری
    totalCurrentAssets: number,
    // * دارایی‌های غیر جاری *
    // حسابها و اسناد دریافتنی بلند مدت
    longTermNotesAndAccountsReceivable: number,
    // سرمایه‌گذاری‌های بلندمدت
    longTermInvestments: number,
    // سرمایه‌گذاری در املاک
    investmentProperty?: number,
    // دارایی‌های نامشهود
    intangibleAssets: number,
    // دارایی‌های ثابت مشهود
    plantAssetsNetOfAccumulatedDepreciation: number,
    // پیش پرداختهای سرمایه ای
    capitalAdvances?: number,
    // سایر دارایی‌ها
    otherNonCurrentAssets: number,
    // جمع دارایی‌های غیرجاری
    totalNonCurrentAssets: number,
    // جمع دارایی‌ها
    totalAssets: number,
    // *** بدهی‌ها ***
    // * بدهی‌های جاری *
    // پرداختنی‌های تجاری و سایر پرداختنی‌ها
    tradeAndNonTradePayables?: number,
    // پرداختنی‌های تجاری
    tradePayables?: number,
    // پرداختنی‌های غیرتجاری
    nonTradePayables?: number,
    // بدهی به شرکتهای گروه و وابسته
    debtToAffiliatedComponies?: number,
    // پیش‌دریافت‌های جاری
    deferredRevenue: number,
    // مالیات پرداختنی
    currentTaxLiabilities: number,
    // سود سهام پرداختنی
    dividentsPayable: number,
    // تسهیلات مالی
    loanPayableCurrent: number,
    // ذخایر
    provisions?: number,
    // بدهی های مرتبط با دارایی های غیر جاری نگهداری شده برای فروش
    liabilitiesOfDisposalGroupsForSale?: number,
    // جمع بدهی‌های جاری
    totalCurrentLiabilities: number,
    // * بدهی‌های غیر جاری *
    // حسابها و اسناد پرداختنی بلند مدت
    longTermPayables: number,
    // پیش‌دریافت‌های غیرجاری
    nonCurrentDeferredRevenue?: number,
    // تسهیلات مالی بلندمدت
    loanPayable: number,
    // ذخیره مزایای پایان خدمت کارکنان
    allowanceForPostRetirement: number,
    // جمع بدهی‌های غیرجاری
    totalNonCurrentLiabilities: number,
    // جمع بدهی‌ها
    totalLiabilities: number,
    // *** حقوق صاحبان سهام ***
    // سرمایه
    commonStock: number,
    // افزایش سرمایه در جریان
    inprocessCapitalIncrease: number,
    // صرف سهام
    sharePremium: number,
    // سهام خزانه
    treasuryStock?: number,
    // صرف سهام خزانه
    treasuryStockConsume?: number,
    // وجوه دریافتی بابت افزایش سرمایه
    receivesForCapitalAdvance?: number,
    // اندوخته قانونی
    legalReserve: number,
    // اندوخته طرح و توسعه / سایر اندوخته‌ها
    expansionReserve: number,
    // سود (زیان) انباشته
    retainedEarnings: number,
    // مازاد تجدید ارزیابی دارایی‌ها
    revaluationSurplus?: number,
    // مازاد تجدید ارزیابی دارایی‌های غیرجاری نگهداری شده برای فروش
    revaluationSurplusOfNonCurrentAssetsForSale?: number,
    // تفاوت تسعیر ناشی از تبدیل به واحد پول گزارشگری
    exchangeDifferences?: number,
    // اندوخته تسعیر ارز دارایی‌ها و بدهی‌های شرکت‌های دولتی
    exchangeReserveOfGovernmentalCorporations?: number,
    // جمع حقوق صاحبان سهام
    totalStockHoldersEquity: number,
    // جمع بدهی‌ها و حقوق صاحبان سهام
    totalLiabilitiesAndStockHoldersEquity: number,
}

export interface ISaleAndCost extends IPeriodic {
    // مبلغ فروش
    sale: number,
    // مبلغ بهاي تمام شده
    cost: number,
}

export interface ICostOfGoods extends IPeriodic {
    // مواد مستقيم مصرفي
    mavadMostaghimMasrafi: number,
    // دستمزدمستقيم توليد
    dastmozdMostaghimTolid: number,
    // سربارتوليد
    sarbarTolid: number,
    // جمع
    jame: number,
    // هزينه جذب نشده درتوليد
    hazinehJazbNashodehDarTolid: number,
    // جمع هزينه هاي توليد
    jameHazinehayeTolid: number,
    // موجودي كالاي درجريان ساخت اول دوره
    mojodiKalaDarJaryanSakhtAvalDoreh: number,
    // موجودي كالاي درجريان ساخت پايان دوره
    mojodiKalaDarJaryanSakhtPayanDoreh: number,
    // ضايعات غيرعادي
    zayeatGhireAdi: number,
    // بهاي تمام شده كالاي توليد شده
    bahayeTamamShodeKalaTolidShode: number,
    // موجودي كالاي ساخته شده اول دوره
    mojodiKalaSakhtehShodeAvalDoreh: number,
    // موجودي كالاي ساخته شده پايان دوره
    mojodiKalaSakhtehShodePayanDoreh: number,
    // بهاي تمام شده كالاي فروش رفته
    bahayeTamamShodeKalaForoshRafteh: number,
    // بهاي تمام شده خدمات ارايه شده
    khadamat: number,
    // جمع بهاي تمام شده
    jameBahayeTamamShode: number,
}
