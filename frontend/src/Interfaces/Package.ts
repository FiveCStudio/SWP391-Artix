
export interface Package {
    packageId: string | number;
    packageName: string;
    packageDescription: string;
}

export interface CurrentPackage {
    currentPackageId: string | number;
    accountId: string | number; // from table account
    packageId: string | number; // from table package
    Date: string | Date; // ISO date
}

