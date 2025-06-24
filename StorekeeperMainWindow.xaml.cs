using System.Diagnostics;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryManagmentApplication
{
    /// <summary>
    /// Логика взаимодействия для StorekeeperMainWindow.xaml
    /// </summary>
    public partial class StorekeeperMainWindow : Window
    {
        DataBaseService dataBaseService = new DataBaseService();

        public StorekeeperMainWindow()
        {
            InitializeComponent();

            // Загрузка данных из базы данных в таблицы //

            dataBaseService.GetWarehouseData(WarehousesTable);

            dataBaseService.GetMaterialData(MaterialsTable);

            dataBaseService.GetRouteData(RoutesTable);

            dataBaseService.GetSupplierData(SuppliersTable);

            dataBaseService.GetProductData(ProductsTable);

            dataBaseService.GetOrderData(OrdersTable);

            dataBaseService.GetTransferOrdersShipmentData(OrdersShipmentTable);

            dataBaseService.GetShipmentDocumentData(ShipmentDocumentsTable);

            dataBaseService.GetTransferOrdersOnTheWayData(OrdersOnTheWayTable);

            dataBaseService.GetOnTheWayDocumentData(OnTheWayDocumentsTable);

            dataBaseService.GetTransferOrdersAcceptanceData(OrdersAcceptanceTable);

            dataBaseService.GetAcceptanceDocumentData(AcceptanceDocumentsTable);

            // //

            // Корректная обработка захвата и фокуса для интерактивной работы с таблицами //

            WarehousesTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(WarehousesTableKeyDown),
            handledEventsToo: true);

            OrdersShipmentTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(OrdersShipmentTableKeyDown),
            handledEventsToo: true);

            OrdersOnTheWayTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(OrdersOnTheWayTableKeyDown),
            handledEventsToo: true);

            // //
        }

        private void WarehousesTableKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {

                var answer = MessageBox.Show("Вы действительно хотите удалить информацию о складе?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (answer == MessageBoxResult.Yes)
                {

                    var selectedWarehouse = (Warehouses)WarehousesTable.SelectedItem;

                    if (selectedWarehouse != null)
                    {
                        int selectedRowNumber = selectedWarehouse.warehouse_id;

                        var warehouseToDelete = dataBaseService.GetWarehouseToDelete(selectedRowNumber);

                        if (warehouseToDelete != null)
                        {
                            dataBaseService.DeleteWarehouse(warehouseToDelete);

                            MessageBox.Show("Информация успешно удалена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        dataBaseService.GetWarehouseData(WarehousesTable);
                    }
                }
                else
                {
                    MessageBox.Show("Данные не были удалены", "Отмена очистки", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }

        private void IsWarehousesEditing(object sender, DataGridCellEditEndingEventArgs e)
        {
            var warehouse = WarehousesTable.SelectedItem as Warehouses;

            if (warehouse != null)
            {
                int quantity = warehouse.capacity - warehouse.free_quantity;

                int capacity = warehouse.capacity;

                if (quantity < 0) MessageBox.Show("Текущее число запасов на складе не может быть отрицательным.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                else
                {
                    if (capacity < 0) MessageBox.Show("Максимальная вместимость склада не может быть отрицательной.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                    else
                    {
                        if (quantity > capacity) MessageBox.Show("Не стоит заполнять склад количеством запасов больше вместительности.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                        else
                        {
                            dataBaseService.UpdateWarehouse(warehouse);
                        }
                    }
                }
            }
        }

        private void AddingWarehouseClick(object sender, RoutedEventArgs e)
        {
            var newWarehouse = new Warehouses()
            {
                capacity = 750000,
                material_id = 1,
                free_quantity = 750000,
                location = "Местоположение"
            };

            dataBaseService.AddWarehouse(newWarehouse);

            dataBaseService.GetWarehouseData(WarehousesTable);

            WarehousesTable.Items.Refresh();
        }

        private void OrdersShipmentTableKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var selectedOrderShipment = (TransferOrdersShipment)OrdersShipmentTable.SelectedItem;

                if (selectedOrderShipment.status == "Выполнен") MessageBox.Show($"Данный заказ уже был отгружен.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                else
                {
                    var answer = MessageBox.Show($"Вы действительно хотите отгрузить и отправить заказ №{selectedOrderShipment.order_id}?", "Подтверждение отправки", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (answer == MessageBoxResult.Yes)
                    {
                        if (selectedOrderShipment != null)
                        {
                            var newSendedOrder = new TransferOrdersOnTheWay
                            {
                                status = "Выполняется",
                                material_id = selectedOrderShipment.material_id,
                                shipment_id = selectedOrderShipment.shipment_id,
                                order_id = selectedOrderShipment.order_id,
                                quantity = selectedOrderShipment.quantity,
                                route_id = selectedOrderShipment.route_id,
                                supplier_id = selectedOrderShipment.supplier_id,
                            };

                            dataBaseService.SendOrder(newSendedOrder);

                            selectedOrderShipment.status = "Выполнен";

                            dataBaseService.UpdateOrder(selectedOrderShipment);

                            var newDocument = new OnTheWayDocuments
                            {
                                material_id = newSendedOrder.material_id,
                                ontheway_id = newSendedOrder.ontheway_id,
                                order_id = newSendedOrder.order_id,
                                quantity = newSendedOrder.quantity,
                                route_id = newSendedOrder.route_id,
                                shipment_id = newSendedOrder.shipment_id,
                                supplier_id = newSendedOrder.supplier_id,
                            };

                            dataBaseService.CreateDocument(newDocument);

                            MessageBox.Show($"Заказ №{selectedOrderShipment.order_id} был отправлен по маршруту №{newSendedOrder.route_id}.", "Отправление заказа", MessageBoxButton.OK, MessageBoxImage.Information);

                            dataBaseService.ChangeStatusSending(selectedOrderShipment.order_id);

                            dataBaseService.GetOrderData(OrdersTable);

                            dataBaseService.GetTransferOrdersShipmentData(OrdersShipmentTable);

                            dataBaseService.GetShipmentDocumentData(ShipmentDocumentsTable);

                            dataBaseService.GetTransferOrdersOnTheWayData(OrdersOnTheWayTable);
                        }
                    }
                    else MessageBox.Show($"Заказ №{selectedOrderShipment.order_id} не был отправлен.", "Отмена действия", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void OrdersOnTheWayTableKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var selectedOrderOnTheWay = (TransferOrdersOnTheWay)OrdersOnTheWayTable.SelectedItem;

                if (selectedOrderOnTheWay.status == "Выполнен") MessageBox.Show($"Данный заказ уже доставлен на приемку.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                else
                {
                    var answer = MessageBox.Show($"Вы действительно хотите принять заказ №{selectedOrderOnTheWay.order_id}?", "Подтверждение приемки", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (answer == MessageBoxResult.Yes)
                    {
                        if (selectedOrderOnTheWay != null)
                        {
                            var newAcceptedOrder = new TransferOrdersAcceptance
                            {
                                status = "Выполняется",
                                
                                ontheway_id = selectedOrderOnTheWay.ontheway_id,
                                material_id = selectedOrderOnTheWay.material_id,
                                shipment_id = selectedOrderOnTheWay.shipment_id,
                                order_id = selectedOrderOnTheWay.order_id,
                                quantity = selectedOrderOnTheWay.quantity,
                                route_id = selectedOrderOnTheWay.route_id,
                                supplier_id = selectedOrderOnTheWay.supplier_id,
                            };

                            dataBaseService.SendOrder(newAcceptedOrder);

                            selectedOrderOnTheWay.status = "Выполнен";

                            dataBaseService.UpdateOrder(selectedOrderOnTheWay);

                            var newDocument = new AcceptanceDocuments
                            {
                                acceptance_id = newAcceptedOrder.acceptance_id,
                                material_id = newAcceptedOrder.material_id,
                                ontheway_id = newAcceptedOrder.ontheway_id,
                                order_id = newAcceptedOrder.order_id,
                                quantity = newAcceptedOrder.quantity,
                                route_id = newAcceptedOrder.route_id,
                                shipment_id = newAcceptedOrder.shipment_id,
                                supplier_id = newAcceptedOrder.supplier_id,
                            };

                            dataBaseService.CreateDocument(newDocument);

                            MessageBox.Show($"Заказ №{newAcceptedOrder.order_id} принят.", "Приемка заказа", MessageBoxButton.OK, MessageBoxImage.Information);

                            dataBaseService.ChangeStatusAcceptance(newAcceptedOrder.order_id);

                            dataBaseService.GetTransferOrdersOnTheWayData(OrdersOnTheWayTable);

                            dataBaseService.GetShipmentDocumentData(ShipmentDocumentsTable);

                            dataBaseService.GetOnTheWayDocumentData(OnTheWayDocumentsTable);

                            dataBaseService.GetOrderData(OrdersTable);

                            dataBaseService.GetTransferOrdersAcceptanceData(OrdersAcceptanceTable);

                            dataBaseService.GetAcceptanceDocumentData(AcceptanceDocumentsTable);
                        }
                    }
                    else MessageBox.Show($"Заказ №{selectedOrderOnTheWay.order_id} не был принят.", "Отмена действия", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
