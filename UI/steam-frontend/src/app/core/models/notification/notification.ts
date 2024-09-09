export interface INotification {
    notificationId: string,
    userId: string,
    subjectName: string,
    notificationType: string,
    dateSent?: Date,
    readStatus: boolean
}

export interface INotificationPaginated {
    notifications: INotification[],
    pageIndex: number,
    totalPages: number,
    totalItems: number
}