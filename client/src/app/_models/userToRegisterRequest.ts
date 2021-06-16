export interface UserToRegisterRequest {
    email: string;
    confirmEmail: string;
    userName: string;
    city: string;
    firstName: string;
    lastName: string;
    password: string;
    confirmPassword: string;
    birthDate: Date;
}
