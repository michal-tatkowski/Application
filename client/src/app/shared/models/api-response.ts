export interface ApiResponse<T> {
  body: T;
}

export interface ApiErrorResponse {
  message: string,
  errorCode: string;
  parameters: string[],
}
