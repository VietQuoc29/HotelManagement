export interface User {
    id: number;
    username: string;
    password: string;
    token?: string;
    roleName?: string;
    fullName?: string;
    email?: string;
    expireTime?: number;
}
