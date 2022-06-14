import { ModuleWithProviders } from '@angular/core';

export interface ICustomer {
    idBarang?: number;
    judul: string;
    jenisBarang: string;
    deskripsi: string;
    idPemilik: number;
    bukaLelang: string;
    tutupLelang: string
}


export interface IPagedResults<T> {
    totalRecords: number;
    results: T;
}

export interface ICustomerResponse {
    status: boolean;
    customer: ICustomer;
}